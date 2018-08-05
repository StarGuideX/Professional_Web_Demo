using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPCoreSamples
{
    /// <summary>
    /// 返回html字符串的帮助类
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// 返回div块
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Div(this string value) =>
            $"<div>{value}</div>";
        /// <summary>
        /// 返回span块
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Span(this string value) =>
            $"{value}";
        /// <summary>
        /// 返回div块（格式Key:Value）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Div(this string key, string value) =>
            $"{key.Span()}:&nbsp;{value.Span()}".Div();

        /// <summary>
        /// 返回一个带url和value的Li
        /// </summary>
        /// <param name="value"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Li(this string value, string url) =>
    $@"<li><a href=""{url}"">{value}</a></li>";

        /// <summary>
        /// 返回一个带value的Li
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Li(this string value) =>
            $@"<li>{value}</li>";

        /// <summary>
        /// 返回一个带value的UL
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Ul(this string value) =>
            $"<ul>{value}</ul>";
    }
}
