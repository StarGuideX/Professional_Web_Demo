using ASPCoreSamples.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPCoreSamples.Controllers
{
    public class HomeController
    {
        private readonly ISampleService _sampleService;
        public HomeController(ISampleService sampleService) =>
            _sampleService = sampleService;

        public async Task Index(HttpContext context) {
            var sb = new StringBuilder();
            sb.Append("<ul>");
            sb.Append(string.Join(string.Empty,
                _sampleService.GetSampleService().Select(s => 
                s.Li()).ToArray()));
            sb.Append("</ul>");
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync(sb.ToString());
        }
    }
}
