using ASPCoreConsoleApp.CustomConfiguration;
using System;

namespace ASPCoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            

            while (true)
            {
                SwitchDemo();
            }

        }

        static void SwitchDemo() {
            Console.WriteLine("1-自定义Configuration");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    new CustomConfigurer().CustomConfigurerStart(new string[] { "section1:key1=settings from command line"});
                    break;
                default:
                    break;
            }
        }

    }
}
