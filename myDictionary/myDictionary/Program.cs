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
                my.Add("a", "b");
                my.Add("a2", "b");
                my.Add("a3", "b");
                my.Remove("a");
                my.Add("b", "check");
                my.Add("b2", "c");
                string value = "";
                if (my.TryGetValue("d",out value))
                {
                    Console.WriteLine(value); 
                }
                else
                {
                    Console.WriteLine("NOT CONTAINS");
                }
                my["b"] = "a";
                Console.WriteLine(my["b"]);
            }
            catch (Exception ex)            
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            } 
        }
    }
}