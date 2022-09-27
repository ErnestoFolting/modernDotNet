using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myDictionary
{
    public class myDict<TKey,TValue>:IDictionary<TKey,TValue>
    {
        private struct Entry
        {
            public int hashCode;
            public int next = -1;
            public TKey key;
            public TValue value;
            public Entry(int hash,TKey k, TValue val)
            {
                hashCode = hash;
                key = k;
                value = val;
            }
        }
        private int[] buckets;
        private Entry[] entries;
        private int freeIndex;
        private int freeCount;
        private int size;
        public event Action<TKey, TValue> added;
        public event Action<TKey, TValue> deleted;
        public event Action<int> cleared;
        public myDict()
        {
            size = 3;
            buckets = new int[size];
            for(int i = 0; i < size;i++)buckets[i] = -1;
            entries = new Entry[size];
            freeIndex = -1;
            freeCount = size;
        }

        public void Add(TKey key, TValue value)
        {
            if (freeCount == 0)
            {
                resize();
            }
            int hash = key.GetHashCode();
            Entry temp = new Entry(hash,key,value);
            int bucketNum = (hash & 0x7fffffff)%buckets.Length;
            int index = buckets[bucketNum];
            if (index != -1)
            {
                temp.next = index;
                do
                {
                    if (entries[index].hashCode == hash && Comparer.Equals(entries[index].key, key))
                    {
                        throw new Exception("Not added. Duplicate element.");
                        return;
                    }
                    index = entries[index].next;
                } while (index != -1 && !entries[index].Equals(default(Entry)));
            }
            if(freeIndex != -1)
            {
                int freeIndexNext = entries[freeIndex].next;
                entries[freeIndex] = temp;
                buckets[bucketNum] = freeIndex;
                freeIndex = freeIndexNext;
            }
            else
            {
                int indexToPut = size - freeCount;
                entries[indexToPut] = temp;
                buckets[bucketNum] = indexToPut;
            }
            added?.Invoke(key, value);
            --freeCount;
        }
        public bool Remove(TKey key)
        {
            int hash = key.GetHashCode();
            int bucketNum = (hash & 0x7fffffff) % buckets.Length;
            int index = buckets[bucketNum];
            if(index != -1)
            {
                bool isFirst = true;
                int prevIndex = index;
                do
                {
                    if (entries[index].hashCode == hash && Comparer.Equals(entries[index].key, key))
                    {
                        if (isFirst)
                        {
                            buckets[bucketNum] = entries[index].next;
                        }
                        else
                        {
                            entries[prevIndex].next = entries[index].next;
                        }
                        entries[index].next = freeIndex;
                        freeIndex = index;
                        TValue val = entries[index].value;
                        entries[index].value = default(TValue);
                        entries[index].key = default(TKey);
                        entries[index].hashCode = -1;
                        freeCount++;
                        deleted?.Invoke(key, val);
                        return true;
                    }
                    if (entries[index].next != -1)
                    {
                        prevIndex = index;
                        index = entries[index].next;
                        isFirst = false;
                    }
                    else break;
                        
                } while (!entries[index].Equals(default(Entry))); 
            }
            throw new Exception("No such an element");
            return false;
        }

        public void resize()
        {
            int oldsize = size;
            size = size * 2 + 1;
            int[] newBuckets = new int[size];
            Entry[] newEntries = new Entry[size];
            freeIndex = -1;
            freeCount = size - oldsize;
            for (int i = 0; i < size; i++) newBuckets[i] = -1;
            for (int i = 0; i < oldsize; i++)
            {
                newEntries[i] = entries[i];
            }
            for(int i = 0; i < oldsize; i++)
            {
                int hash = newEntries[i].hashCode;
                if (hash != -1)
                {
                    int bucketNum = (hash & 0x7fffffff) % newBuckets.Length;
                    int index = newBuckets[bucketNum];
                    newEntries[i].next = index;
                    newBuckets[bucketNum] = i;
                }
            }
            buckets = newBuckets;
            entries = newEntries;
        }
        public bool ContainsKey(TKey keyToFind)
        {
            int hash = keyToFind.GetHashCode();
            int bucketNum = (hash & 0x7fffffff) % buckets.Length;
            int index = buckets[bucketNum];
            if (index != -1)
            {
                do
                {
                    if (entries[index].hashCode == hash && Comparer.Equals(entries[index].key, keyToFind))
                    {
                        return true;
                    }
                    index = entries[index].next;
                } while (index != -1 && !entries[index].Equals(default(Entry)));
            }
            return false;
        }
        public bool TryGetValue(TKey keyToFind, out TValue value)
        {
            if (ContainsKey(keyToFind)){
                int hash = keyToFind.GetHashCode();
                int bucketNum = (hash & 0x7fffffff) % buckets.Length;
                int index = buckets[bucketNum];
                do
                {
                    if (entries[index].hashCode == hash && Comparer.Equals(entries[index].key, keyToFind))
                    {
                        value = entries[index].value;
                        return true;
                    }
                    index = entries[index].next;
                } while (index != -1 && !entries[index].Equals(default(Entry)));
            }
            value = default(TValue);
            return false;
        }
        public TValue this[TKey key]
        {
            get
            {
                TValue value;
                TryGetValue(key, out value);
                if (Comparer.Equals(value,default(TValue))) throw new Exception("No element at that index.");
                return value;
            }
            set
            {
                int hash = key.GetHashCode();
                int bucketNum = (hash & 0x7fffffff) % buckets.Length;
                int index = buckets[bucketNum];
                if (index != -1)
                {
                    do
                    {
                        if (entries[index].hashCode == hash && Comparer.Equals(entries[index].key, key))
                        {
                            entries[index].value = value;
                            return;
                        }
                        index = entries[index].next;
                    } while (index != -1 && !entries[index].Equals(default(Entry)));
                }
                throw new Exception("Invalid index while setting a value.");
            }
        }
        public ICollection<TKey> Keys
        {
            get
            {
                ICollection<TKey> keys = new List<TKey>();    
                for (int i = 0; i < size; i++)
                {
                    if(!Comparer.Equals(entries[i].key, default(TKey)))
                    {
                        keys.Add(entries[i].key);
                    }
                    
                }
                return keys;
            }
        }
        public ICollection<TValue> Values
        {
            get
            {
                ICollection<TValue> values = new List<TValue>();
                for (int i = 0; i < size; i++)
                {
                    if (!Comparer.Equals(entries[i].value, default(TValue)))
                    {
                        values.Add(entries[i].value);
                    }
                }
                return values;
            }
        }
        public void Add(KeyValuePair<TKey,TValue> pair)
        {
            Add(pair.Key, pair.Value);
        }
        public void Clear()
        {
            size = 3;
            buckets = new int[size];
            for (int i = 0; i < size; i++) buckets[i] = -1;
            entries = new Entry[size];
            freeIndex = -1;
            freeCount = size;
            cleared?.Invoke(size);
        }
        public bool Contains(KeyValuePair<TKey, TValue> pair)
        {
            int hash = pair.Key.GetHashCode();
            int bucketNum = (hash & 0x7fffffff) % buckets.Length;
            int index = buckets[bucketNum];
            if (index != -1)
            {
                do
                {
                    if (entries[index].hashCode == hash && Comparer.Equals(entries[index].key, pair.Key) && Comparer.Equals(entries[index].value, pair.Value))
                    {
                        return true;
                    }
                    index = entries[index].next;
                } while (index != -1 && !entries[index].Equals(default(Entry)));
            }
            return false;
        }
        public bool Remove(KeyValuePair<TKey, TValue> pair)
        {
            return Remove(pair.Key);
        }
        public int Count {
            get
            {
                return(size-freeCount);
            } 
        }
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        public void CopyTo(KeyValuePair<TKey, TValue>[] res, int fromIndex)
        {
            if ((size - freeCount) > res.Length - fromIndex || fromIndex < 0)
            {
                throw new ArgumentException();
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    if (!Comparer.Equals(entries[i].value, default(TValue)))
                    {
                        KeyValuePair<TKey, TValue> temp = new KeyValuePair<TKey, TValue>(entries[i].key, entries[i].value);
                        res[fromIndex] = temp;
                        fromIndex++;
                    }
                }
            }
        }
        public IEnumerator<KeyValuePair<TKey,TValue>> GetEnumerator()
        {
            foreach(var el in entries)
            {
                if (!Comparer.Equals(el.value, default(TValue)))
                {
                    yield return new KeyValuePair<TKey, TValue>(el.key, el.value);
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
