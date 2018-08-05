using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASPCoreConsoleApp.CustomConfiguration
{
    public class CustomConfigurer
    {
        public void CustomConfigurerStart(string[] args) {

            SetupConfiguration(args);
            ReadConfiguration();
        }


        public void SetupConfiguration(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .AddCommandLine(args);
            Configuration = builder.Build();
        }
        public static IConfigurationRoot Configuration
        {
            get;
            private set;
        }

        private void ReadConfiguration()
        {
            string val1 = Configuration.GetSection("section1")["key1"];
            Console.WriteLine(val1);
            string val2 = Configuration.GetSection("section1")["key2"];
            Console.WriteLine(val2);

        }
    }
}
