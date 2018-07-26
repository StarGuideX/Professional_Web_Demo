using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Random rd = new Random();
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine(rd.Next(1, 11));
            }

            Console.ReadLine();
        }
    }
}
