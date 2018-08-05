using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ASPCoreSamples
{
    public static class RequestAndResponseSamples
    {
        /// <summary>
        /// 获取HttpRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// HttpRequest.Query
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string QueryString(HttpRequest request)
        {
            string xtext = request.Query["x"];
            string ytext = request.Query["y"];

            if (xtext == null || ytext == null)
            {
                return "x and y must be set".Div();
            }

            if (!int.TryParse(xtext, out int x))
            {
                return $"Error parsing {xtext}".Div();
            }

            if (!int.TryParse(ytext, out int y))
            {
                return $"Error parsing {ytext}".Div();
            }

            return $"{x} + {y} = {x + y}".Div();
        }

        /// <summary>
        /// HttpRequest.Query["data"]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string Content(HttpRequest request) => request.Query["data"];
        /// <summary>
        /// HttpRequest.Query["data"]，使用默认编码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string ContentEncoded(HttpRequest request) => HtmlEncoder.Default.Encode(request.Query["data"]);

        /// <summary>
        /// get请求转换为post
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetForm(HttpRequest request)
        {
            string result = string.Empty;
            switch (request.Method)
            {
                case "GET":
                    result = GetForm();
                    break;
                case "POST":
                    result = ShowForm(request);
                    break;
                default:
                    break;
            }
            return result;
        }
        /// <summary>
        /// get 获取表单
        /// </summary>
        /// <returns></returns>
        private static string GetForm() =>
                    "<form method=\"post\" action=\"form\">" +
                    "<input type=\"text\" name=\"text1\" />" +
                    "<input type=\"submit\" value=\"Submit\" />" +
                    "</form>";

        /// <summary>
        /// 利用表单转为post
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static string ShowForm(HttpRequest request)
        {
            var sb = new StringBuilder();
            if (request.HasFormContentType)
            {
                IFormCollection coll = request.Form;
                foreach (var key in coll.Keys)
                {
                    sb.Append(key.Div(HtmlEncoder.Default.Encode(coll[key])));
                }
                return sb.ToString();
            }
            else
            {
                return "no form".Div();
            }
        }

        /// <summary>
        /// Cookie写入
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string WriteCookie(HttpResponse response)
        {
            response.Cookies.Append("color", "red", new CookieOptions
            {
                Path = "/cookis",
                Expires = DateTime.Now.AddDays(1)
            });
            return "cookie written".Div();
        }

        /// <summary>
        /// Cookie读取
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string ReadCookie(HttpRequest request)
        {
            var sb = new StringBuilder();
            IRequestCookieCollection cookies = request.Cookies;
            foreach (var key in cookies.Keys)
            {
                sb.Append(key.Div(cookies[key]));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 得到json
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string GetJson(HttpResponse response)
        {
            var b = new
            {
                Tittle = "Professinoal C# 7",
                publisher = "Wrox Press",
                Author = "Christian Nagel"
            };

            string json = JsonConvert.SerializeObject(b);
            response.ContentType = "application/json";
            return json;
        }
    }
}
