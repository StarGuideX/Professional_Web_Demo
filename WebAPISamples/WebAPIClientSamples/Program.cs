using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebAPIClientSamples.Services;
using Microsoft.Extensions.Logging;

namespace WebAPIClientSamples
{
    class Program
    {
        public static IServiceProvider Container { get; private set; }

        static async void Main(string[] args)
        {
            Console.WriteLine("等待服务......");
            Console.ReadKey();
            RegisterServices();
            var test = Container.GetRequiredService<SampleRequestClient>();

            await test.ReadChaptersAsync();
            await test.ReadChapterAsync();
            await test.ReadNotExistingChapterAsync();
            await test.ReadXmlAsync();
            await test.AddChapterAsync();
            await test.UpdateChapterAsync();
            await test.RemoveChapterAsync();
            Console.ReadLine();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<UrlService>();
            services.AddSingleton<BookChapterClientService>();
            services.AddTransient<SampleRequestClient>();

            Container = services.BuildServiceProvider();
        }
    }
}
