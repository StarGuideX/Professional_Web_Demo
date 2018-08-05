using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreSamples
{
    public class SubSection1
    {
        public string Setting4 { get; set; }
    }

    public class AppSettings
    {
        public string Setting2 { get; set; }
        public string Setting3 { get; set; }
        public SubSection1 SubSection1 { get; set; }
    }

    public class ConfigurationSample
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// ConfigurationSample通过依赖注入访问IConfiguration接口。
        /// </summary>
        /// <param name="configuration"></param>
        public ConfigurationSample(IConfiguration configuration) =>
            _configuration = configuration;

        /// <summary>
        /// 访问Setting，_configuration.GetSection("SampleSettings")["Setting1"];
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ShowApplicationSettingsAsync(HttpContext context)
        {
            //通过GetSection获得配置文件中的Sections，通过索引器获得Settings，
            string settings = _configuration.GetSection("SampleSettings")["Setting1"];
            await context.Response.WriteAsync(settings.Div());
        }

        /// <summary>
        /// 访问Setting，借助冒号。_configuration["SampleSettings:Setting1"];
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ShowApplicationSettingsUsingColonsAsync(HttpContext context)
        {
            string settings = _configuration["SampleSettings:Setting1"];
            await context.Response.WriteAsync(settings.Div());
        }

        /// <summary>
        /// 调用泛型Get方法和AppSettings类得到自定义类映射的自定义configuration类型
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ShowApplicationSettingsStronglyTyped(HttpContext context)
        {
            
            AppSettings settings = _configuration.GetSection("AppSettings").Get<AppSettings>();
            await context.Response.WriteAsync($"setting 2:{settings.Setting2}" +
                $"setting 3:{settings.Setting3}" +
                $"setting 4:{settings.SubSection1.Setting4}".Div());
        }

        /// <summary>
        /// 使用GetConnectionString直接访问数据库连接串
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ShowConnectionStringSettingAsync(HttpContext context)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            await context.Response.WriteAsync(connectionString.Div());
        }

    }
}
