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

        /// <summary>
        /// 为ConfigurationBuilder添加了JSON、环境变量、命令行Prociders
        /// </summary>
        /// <param name="args"></param>
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
        /// <summary>
        /// 读取配置文件-appsettings.json
        /// </summary>
        private void ReadConfiguration()
        {
            string val1 = Configuration.GetSection("section1")["key1"];
            Console.WriteLine(val1);
            string val2 = Configuration.GetSection("section1")["key2"];
            Console.WriteLine(val2);

        }
    }
}
