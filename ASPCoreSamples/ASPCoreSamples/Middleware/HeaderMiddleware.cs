using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ASPCoreSamples.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class HeaderMiddleware
    {
        /// <summary>
        /// RequestDelegate是一个委托，它接收HttpContext作为参数并返回一个Task。 
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 该构造函数接收对下一个中间件类型的引用。 
        /// </summary>
        /// <param name="next"></param>
        public HeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 可以访问request和response,
        /// 作为最后一个操作，Invoke方法调用下一个中间件模块
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("sampleheader",
                new[] { "addheadermiddleware" });
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseHeaderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HeaderMiddleware>();
        }
    }
}
