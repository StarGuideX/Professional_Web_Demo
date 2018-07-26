using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPCoreSamples
{
    public static class RequestAndResponseSamples
    {
        /// <summary>
        /// 获取HttpRequest Headers
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetHeaderInformation(HttpRequest request)
        {
            var sb = new StringBuilder();
            foreach (var header in request.Headers)
            {
                sb.Append(header.Key.Div(string.Join("; ", header.Value)));
            }
            return sb.ToString();
        }

        public static string QueryString(HttpRequest request)
        {
            string xtext = request.Query["x"];
            string ytext = request.Query["y"];

            if (xtext == null || ytext == null)
            {
                return "x and y must be set".Div();
            }

            if (!int.TryParse(xtext,out int x))
            {
                return $"Error parsing {xtext}".Div();
            }

            if (!int.TryParse(ytext, out int y))
            {
                return $"Error parsing {ytext}".Div();
            }

            return $"{x} + {y} = {x + y}".Div();
        }

    }
}
