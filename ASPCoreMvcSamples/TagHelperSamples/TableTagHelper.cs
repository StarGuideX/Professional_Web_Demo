using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TagHelperSamples
{
    /// <summary>
    /// 这个HtmlTargetElement指定table应用本helper和属性items
    /// 此attribute用于设置HtmlAttributeName属性指定的Items属性
    /// </summary>
    [HtmlTargetElement("table", Attributes = ItemsAttributeName)]
    public class TableTagHelper : TagHelper
    {
        private const string ItemsAttributeName = "items";
        [HtmlAttributeName(ItemsAttributeName)]
        public IEnumerable<object> Items { get; set; }

        /// <summary>
        /// 
        /// 备注：如果使用Tag Helper时指定了table元素，已经定义了行和列，可以将结果与现有内容合并。
        /// </summary>
        /// <param name="context">TagHelperContext包含应用了Tag Helper的HTML元素的属性以及所有子元素</param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));
            // TagBuilder帮助创建具有属性的html元素
            var table = new TagBuilder("table");

            table.GenerateId(context.UniqueId, "id");
            var attributes = context.AllAttributes
            .Where(a => a.Name != ItemsAttributeName)
            .ToDictionary(a => a.Name);
            //向TagBuilder添加属性，需要所有属性名称及其值的字典
            table.MergeAttributes(attributes);
            PropertyInfo[] properties = CreateHeading(table);



            base.Process(context, output);
        }

        private PropertyInfo[] CreateHeading(TagBuilder table)
        {
            var tr = new TagBuilder("tr");
            var heading = Items.First();
            PropertyInfo[] properties = heading.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var th = new TagBuilder("th");
                th.InnerHtml.Append(prop.Name);
                tr.InnerHtml.AppendHtml(th);
            }
            table.InnerHtml.AppendHtml(tr);
            return properties;
        }
    }
}
