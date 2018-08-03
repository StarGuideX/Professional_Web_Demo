using System;

namespace ConsoleApp1
{
    class Program
    {
        class myClass {

            string textStr;

            public string TextStr { get => textStr; set => textStr = value; }
        }




        static void Main(string[] args)
        {
            myClass my = new myClass();
            my.TextStr = "ssssssssss";


            Console.WriteLine($"my.TextStr.equals{my.TextStr.Equals("ssssssssss")}");
            Console.WriteLine($"my.TextStr =={my.TextStr=="ssssssssss"}");

            Console.ReadLine();
        }
    }
}
