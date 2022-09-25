using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myDictionary
{
    public class myDict<TKey,TValue>
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
            Console.WriteLine("Added to Bucket {0} with Hash {1}",bucketNum,hash);
            int index = buckets[bucketNum];
            
            if (index != -1)
            {
                temp.next = index;
                do
                {
                    if (entries[index].hashCode == hash && Comparer.Equals(entries[index].key, key))
                    {
                        entries[index].value = value;
                        throw new ArgumentException();
                        return;
                    }
                    index = entries[index].next;
                } while (index != -1 && entries[index].next != -1);
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
            Console.WriteLine("Free is {0}",freeIndex);
            --freeCount;
        }
        public bool Remove(TKey key)
        {
            int hash = key.GetHashCode();
            int bucketNum = (hash & 0x7fffffff) % buckets.Length;
            Console.WriteLine("DELETED Bucket {0} with Hash {1}", bucketNum, hash);
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
                        Console.WriteLine("free index override {0}",freeIndex);
                        entries[index].next = freeIndex;
                        freeIndex = index;
                        entries[index].value = default(TValue);
                        entries[index].key = default(TKey);
                        entries[index].hashCode = -1;
                        freeCount++;
                        return true;
                    }
                    if (entries[index].next != -1)
                    {
                        prevIndex = index;
                        index = entries[index].next;
                        isFirst = false;
                    }
                        
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
                Console.WriteLine("Check HASH {0}",hash);
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
    }
}
