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
                //myDict<string, string> my = new myDict<string, string>();
                //my.Add("a", "b");
                //my.Add("a2", "b");
                //my.Add("a3", "b");
                //my.Remove("a");

                //string value = "";
                //if (my.TryGetValue("d", out value))
                //{
                //    Console.WriteLine(value);
                //}
                //else
                //{
                //    Console.WriteLine("NOT CONTAINS");
                //}
                //my["a2"] = "a";
                //Console.WriteLine();
                //foreach (var el in my.Values)
                //{
                //    Console.WriteLine("{0}el", el);
                //}
                //KeyValuePair<string, string>[] temp = new KeyValuePair<string, string>[5];
                //my.CopyTo(temp, 0);
                //Console.WriteLine();
                //foreach (var el in temp)
                //{
                //    Console.WriteLine("{0} - {1}", el.Key, el.Value);
                //}
                //Console.WriteLine();
                //foreach (var el in my)
                //{
                //    Console.WriteLine("{0} --- {1}", el.Key, el.Value);
                //}
                myDict<string, string> my2 = new()
                {
                    { "a", "a" },
                    { "b", "b" },
                    { "c", "c" },
                    { "d", "d" },
                    { "e", "e" }
                };
                my2.added += addedHandler;
                my2.deleted += removedHandler;
                my2.cleared += clearedHandler;
                my2.Remove("b");
                foreach (var el in my2)
                {
                    Console.WriteLine("{0} --- {1}", el.Key, el.Value);
                }
                my2.Clear();
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