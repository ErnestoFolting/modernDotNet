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
                my.Add("a", "a");
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