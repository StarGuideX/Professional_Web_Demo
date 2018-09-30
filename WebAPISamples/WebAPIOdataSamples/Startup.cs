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
            sampleBooks.CreateDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseHsts();
            //}

            var builder = new ODataConventionModelBuilder(app.ApplicationServices);
            builder.EntitySet<Book>("Books");
            builder.EntitySet<BookChapter>("BookChapter");
            //app.UseHttpsRedirection();
            app.UseMvc(routeBuilder=> {
                routeBuilder.MapODataServiceRoute("ODataRoute", "odata", builder.GetEdmModel());
                routeBuilder.EnableDependencyInjection();
            });
        }
    }
}
