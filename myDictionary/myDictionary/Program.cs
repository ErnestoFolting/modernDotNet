using System;
using System.Collections.Generic;
using myDictionary;

namespace Lab1
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                myDict<int, string> dict = new myDict<int, string>()
                {
                    {1,"a"},
                    {2,"b"},
                    {3,"c"},
                };
                dict.added += addedHandler;
                dict.removed += removedHandler;
                dict.cleared += clearedHandler;

                dict.Add(-1,"a");
                dict.Add(5,"e");
                Console.WriteLine("\nForeach: ");
                foreach (var el in dict)
                {
                    Console.WriteLine("{0} - {1}", el.Key, el.Value);
                }

                dict.Remove(5);

                if (dict.ContainsKey(5))
                {
                    Console.WriteLine("Contains");
                }
                else
                {
                    Console.WriteLine("Not contains");
                }

                string res = "";
                if (dict.TryGetValue(3, out res)) Console.WriteLine("Got value: {0}",res);

                dict[2] = "a";
                Console.WriteLine("dict[2] - {0}",dict[2]);

                Console.WriteLine();
                ICollection<int> keys = dict.Keys;
                foreach (int key in keys)
                {
                    Console.WriteLine("Key - {0}",key);
                }

                Console.WriteLine("\nElements count: {0}", dict.Count);

                KeyValuePair<int, string>[] copied = new KeyValuePair<int, string>[4];
                dict.CopyTo(copied, 0);

                foreach(var item in copied)
                {
                    Console.WriteLine("Copied {0} - {1}",item.Key,item.Value);
                }

                dict.Clear();

                KeyValuePair<int,string> temp = new KeyValuePair<int, string>(0, "x");
                if (dict.Contains(temp))
                {
                    Console.WriteLine("\nContains");
                }
                else
                {
                    Console.WriteLine("\nNot contains");
                }
            }
            catch (Exception ex)            
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            } 
        }
        static void addedHandler<TKey, TValue>(TKey key, TValue val) => Console.WriteLine("Added: {0} - {1}",key, val);
        static void removedHandler<TKey, TValue>(TKey key, TValue val) => Console.WriteLine("Removed: {0} - {1}",key, val);
        static void clearedHandler(int newSize) => Console.WriteLine("The collection has been cleared. The new size is {0}",newSize);
    }
}