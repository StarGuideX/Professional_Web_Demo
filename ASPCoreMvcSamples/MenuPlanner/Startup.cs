using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Data;
using MenuPlanner.Models;
using MenuPlanner.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MenuPlanner
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            #region 身份验证
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            #endregion
            services.AddDbContext<MenuCardsContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IMenuCardsService, MenuCardsService>();
            #region 身份验证
            // AddIdentity - 注册身份验证
            // AddIdentity方法映射身份服务使用的用户类型(ApplicationUser)和角色类(IdentityRole)
            // ApplicationUser 派生自IdentityUser ; IdentityRole是一个基于字符串的角色类，派生自IdentityRole<string>。 
            // AddIdentity方法的重载方法可以使用双因素身份验证配置身份系统;
            //  邮件令牌提供商(email token providers);用户选项(user options)，
            //  例如需要唯一的电子邮件;或需要用户名匹配的正则表达式。
            // AddIdentity返回一个IdentityBuilder，它允许身份系统的其他配置，
            //  例如使用的实体框架上下文(AddEntityFrameworkStores)
            //  和令牌提供程序(AddDefaultTokenProviders)。
            //  可以添加的其他提供程序包括错误(errors)，
            //  密码验证程序(password validators)，
            //  角色管理器(role managers)，
            //  用户管理器(user managers)和用户验证程序(user validators)。
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<IEmailSender, EmailSender>();
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
