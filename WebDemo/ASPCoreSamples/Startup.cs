using ASPCoreSamples.Controllers;
using ASPCoreSamples.Middleware;
using ASPCoreSamples.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ASPCoreSamples
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // 使用AddTransient，把类DefaultSampleService映射到ISampleService
            // 当使用ISampleService接口，DefaultSampleService类会实例化
            // AddTransient,每次注入服务时都会重新实例化该服务。
            services.AddTransient<ISampleService, DefaultSampleService>();
            // AddSingleton,只实例化一次，每次注入服务都会使用相同的实例
            //services.AddSingleton<ISampleService, DefaultSampleService>();
            services.AddTransient<HomeController>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHeaderMiddleware();

            //使用HttpRequest.Path定义轻量级自定义路由
            app.Map("/RequestAndResponse", app1 =>
            {
                app1.Run(async context =>
                {
                    context.Response.ContentType = "text/html";
                    string result = string.Empty;

                    switch (context.Request.Path.Value.ToLower())
                    {
                        case "/header":
                            result = RequestAndResponseSamples.GetHeaderInformation(context.Request);
                            break;
                        case "/add":
                            result = RequestAndResponseSamples.QueryString(context.Request);
                            break;
                        case "/content":
                            result = RequestAndResponseSamples.Content(context.Request);
                            break;
                        case "/encoded":
                            result = RequestAndResponseSamples.ContentEncoded(context.Request);
                            break;
                        case "/form":
                            result = RequestAndResponseSamples.GetForm(context.Request);
                            break;
                        case "/writecookie":
                            result = RequestAndResponseSamples.WriteCookie(context.Response);
                            break;
                        case "/readcookie":
                            result = RequestAndResponseSamples.ReadCookie(context.Request);
                            break;
                        case "/json":
                            result = RequestAndResponseSamples.GetJson(context.Response);
                            break;
                        default:
                            result = RequestAndResponseSamples.GetRequestInformation(context.Request);
                            break;
                    }
                    await context.Response.WriteAsync(result);
                });
            });

            app.Map("/home", homeApp =>
            {
                homeApp.Run(async (context) =>
                {
                    HomeController controller = homeApp.ApplicationServices.GetService<HomeController>();
                    await controller.Index(context);
                });
            });

            app.MapWhen(context =>
            context.Request.Path.Value.Contains("hello"),
            helloApp =>
            {
                helloApp.Run(async context =>
                {
                    await context.Response.WriteAsync("hello in the path".Div());

                });
            });


            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });

        }
    }
}
