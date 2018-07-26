using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPCoreSamples
{
    public static class HtmlExtensions
    {
        public static string Div(this string value) =>
            $"<div>{value}</div>";
        public static string Span(this string value) =>
            $"{value}";
        public static string Div(this string key, string value) =>
            $"{key.Span()}:&nbsp;{value.Span()}".Div();
        public static string GetRequestInformation(HttpRequest request)
        {
            var sb = new StringBuilder();
            sb.Append("scheme".Div(request.Scheme));
            sb.Append("hots".Div(request.Host.HasValue ? request.Host.Value : "no host"));
            sb.Append("path".Div(request.Path));
            sb.Append("QueryString".Div(request.QueryString.HasValue ? request.QueryString.Value : "no scheme"));
            sb.Append("method".Div(request.Method));
            sb.Append("protocol".Div(request.Protocol));
            return sb.ToString();
        }


    }
}
