using Markdig;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TagHelperSamples
{
    /// <summary>
    /// HtmlTargetElement特性用于指定用于指定Tag Helper的元素或者属性的名称
    /// </summary>
    [HtmlTargetElement("markdown", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement(Attributes = "markdownfile")]
    public class MarkdownTagHelper : TagHelper
    {
        /// <summary>
        ///  当使用HtmlAttributeName属性注释时，
        ///  基础结构会自动应用Tag Helper的属性。 
        ///  这里，属性MarkdownFile从markdownfile属性获取其值
        /// </summary>
        [HtmlAttributeName("markdownfile")]
        public string MarkdownFile { get; set; }

        private readonly IHostingEnvironment _env;

        //  Tag Helper可以使用依赖注入。 
        // 因为MarkdownTagHelper需要wwwroot文件的目录，
        // 并且该目录是从IHostingEnvironment接口返回的，
        // 所以此接口将在构造函数中注入
        public MarkdownTagHelper(IHostingEnvironment env) => _env = env;

        /// <summary>
        /// 需要实现Process或者ProcessAsync。
        /// 
        /// 考虑了MarkdownTagHelper的两种不同用途。
        /// 一种用法是指定markdown元素，其中内容作为元素的子元素，另一个是markdownfile属性引用Markdown文件。
        /// 
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            string markdown = string.Empty;
            // 使用属性markdownfile，则设置MarkdownFile属性
            if (MarkdownFile != null)
            {
                //WebRootPath返回Web文件的根路径。
                string fileName = Path.Combine(_env.WebRootPath, MarkdownFile);
                markdown = File.ReadAllText(fileName);
            }
            // 使用markdown元素读取此元素的内容
            else
            {
                // 要检索内容，需要调用GetChildContentAsync方法，
                // 并且在此方法返回后，需要调用GetContent方法，该方法最终返回HTML页面中指定的内容。
                markdown = (await output.GetChildContentAsync()).GetContent();
            }
            //  使用Markdig库的Markdown类，Markdown内容将转换为HTML。
            // 然后将此HTML代码放入调用SetHtmlContent方法的TagHelperOutput的内容中
            string x = Markdown.ToHtml(markdown);


            output.Content.SetHtmlContent(Markdown.ToHtml(markdown));
        }
    }
}
