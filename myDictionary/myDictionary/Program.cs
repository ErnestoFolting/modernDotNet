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
                myDict<string, string> my = new myDict<string, string>();
                my.Add("a", "a");
                my.Add("b", "b");
                my.Add("c", "c");
                my.Remove("b");
                my.Remove("c");
                my.Add("d", "d");
                my.Add("e", "d");
                my.Add("f", "d");
                my.Add("k", "d");
                my.Remove("a");
                my.Remove("f");
                my.Remove("k");
                my.Add("test1","a");
                my.Add("test2","a");
                my.Add("test3","a");
                my.Add("test4","a");
                my.Add("test5","a");
                my.Add("test6","a");
                my.Add("test7","a");
                my.Add("test8","a");
                my.Add("test9","a");
                my.Add("test10","a");
                my.Add("test11","a");
                my.Add("test12","a");
                my.Add("test13","a");
                my.Add("test14","a");
                my.Add("test15","a");
                my.Remove("test1");
                my.Remove("test2");
                my.Remove("d");
                my.Remove("test15");
                my.Remove("test14");
                my.Remove("test13");
                my.Add("test15","test");
                my.Add("test14","test");
                my.Add("test13","test");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Duplicate key while adding.");
            } 
        }
    }
}