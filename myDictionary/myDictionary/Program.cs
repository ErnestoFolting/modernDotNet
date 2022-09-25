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

                string value = "";
                if (my.TryGetValue("d",out value))
                {
                    Console.WriteLine(value); 
                }
                else
                {
                    Console.WriteLine("NOT CONTAINS");
                }
                my["a2"] = "a";
                Console.WriteLine();
                foreach(var el in my.Values)
                {
                    Console.WriteLine("{0}el",el) ;
                }
                
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