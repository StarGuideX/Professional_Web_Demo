using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAPIOdataSamples.Models;
using WebAPIOdataSamples.Services;

namespace WebAPIOdataSamples
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
            services.AddMvc();// SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<CreateBooksService> ();
            services.AddDbContext<BooksContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("BooksConnection"));
            });
            services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CreateBooksService sampleBooks)
        {
            //如果数据库不存在,则创建
            sampleBooks.CreateDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseHsts();
            //}

            // ODataConventionModelBuilder将.NET类映射到实体数据模型（Entity Data Model-EDM）
            // OData使用EDM模型来定义服务公开的数据
            var builder = new ODataConventionModelBuilder(app.ApplicationServices);
            builder.EntitySet<Book>("Books");
            builder.EntitySet<BookChapter>("Chapters");
            //app.UseHttpsRedirection();
            app.UseMvc(routeBuilder=> {
                // routeName: 路由的名称
                // routePrefix: 路由前缀
                // model: ODataConventionModelBuilder创建模型后返回的IEdmModel
                routeBuilder.MapODataServiceRoute(routeName:"ODataRoute",routePrefix:"odata", model:builder.GetEdmModel());
                routeBuilder.EnableDependencyInjection();
            });
        }
    }
}
