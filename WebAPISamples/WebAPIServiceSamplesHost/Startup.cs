using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAPIServiceSamples.BookServices.Models;
using WebAPIServiceSamples.BookServices.Services;

namespace WebAPIServiceSamplesHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IMvcBuilder mvcBuilder = services.AddMvc();
            mvcBuilder.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region 改变响应格式
            mvcBuilder.AddXmlSerializerFormatters();
            #endregion
            #region 注册服务
            services.AddDbContext<BooksContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("BooksConnection"));
            });



            // BookChaptersService作为单例注册，所以可以同时从多个线程访问它;
            // 这就是为什么在实现中需要ConcurrentDictionary的原因
            //services.AddSingleton<IBookChaptersService, BookChaptersService>();

            services.AddSingleton<IBookChaptersService, DBBookChaptersService>();

            services.AddSingleton<SampleChapters>();

            #endregion
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,SampleChapters sampleChapters, BooksContext booksContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();

            bool created = booksContext.Database.EnsureCreated();
            if (created)
            {
              //await sampleChapters.CreateSampleChaptersAsync();
            }
            
        }
    }
}
