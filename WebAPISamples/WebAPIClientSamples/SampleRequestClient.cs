using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebAPIClientSamples.Models;
using WebAPIClientSamples.Services;

namespace WebAPIClientSamples
{
    public class SampleRequestClient
    {
        private readonly UrlService _urlService;
        private readonly BookChapterClientService _client;

        public SampleRequestClient(UrlService urlService, BookChapterClientService client)
        {
            _urlService = urlService ?? 
                throw new ArgumentNullException(nameof(urlService));
            _client = client ?? 
                throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// 查询所有章节，并打印
        /// </summary>
        /// <returns></returns>
        public async  Task ReadChaptersAsync()
        {
            Console.WriteLine(nameof(ReadChaptersAsync));
            IEnumerable<BookChapter> chapters = await _client.GetAllAsync(_urlService.BooksApi);
            foreach (BookChapter chapter in chapters)
            {
                Console.WriteLine(chapter.Title);
            }
        }

        /// <summary>
        /// 如果章节的标识符不存在，就调用
        /// </summary>
        /// <returns></returns>
        public async Task ReadNotExistingChapterAsync()
        {
            Console.WriteLine(nameof(ReadNotExistingChapterAsync));
            string requestedIdentifier = Guid.NewGuid().ToString();
            try
            {
                BookChapter chapter = await _client.GetAsync(_urlService.BooksApi + requestedIdentifier.ToString());
                Console.WriteLine($"{chapter.Number} {chapter.Title}");
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("404"))
            {
                Console.WriteLine($"没有找到章节识别码: {requestedIdentifier}");
            }
        }


        /// <summary>
        /// 更新Title为title的章节
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task UpdateChapterAsync(string title)
        {
            Console.WriteLine(nameof(UpdateChapterAsync));
            var chapters = await _client.GetAllAsync(_urlService.BooksApi);
            var chapter = chapters.SingleOrDefault(c => c.Title == title);
            if (chapter != null)
            {
                chapter.Number = 32;
                chapter.Title = "Windows Apps";
                await _client.PutAsync(_urlService.BooksApi + chapter.Id, chapter);
                Console.WriteLine($"更新章节{chapter.Title}");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// 删除Title为title的章节
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task RemoveChapterAsync(string title)
        {
            Console.WriteLine(nameof(RemoveChapterAsync));
            var chapters = await _client.GetAllAsync(_urlService.BooksApi);
            var chapter = chapters.SingleOrDefault(c => c.Title == title);
            if (chapter != null)
            {
                await _client.DeleteAsync(_urlService.BooksApi + chapter.Id);
                Console.WriteLine($"删除章节{chapter.Title}");
            }
            Console.WriteLine();
        }
        /// <summary>
        /// 添加一个BookChapter
        /// </summary>
        /// <returns></returns>
        public async Task AddChapterAsync()
        {
            Console.WriteLine(nameof(AddChapterAsync));
            BookChapter chapter = new BookChapter
            {
                Number = 42,
                Title = "ASP.NET Web API",
                Pages = 35
            };
            chapter = await _client.PostAsync(_urlService.BooksApi, chapter);
            Console.WriteLine($"已增加章节{chapter.Title},Id: {chapter.Id}");
            Console.WriteLine();
        }

        /// <summary>
        /// xml
        /// </summary>
        /// <returns></returns>
        public async Task ReadXmlAsync()
        {
            Console.WriteLine(nameof(ReadXmlAsync));
            XElement chapters = await _client.GetAllXmlAsync(_urlService.BooksApi);
            Console.WriteLine(chapters);
            Console.WriteLine();
        }

        /// <summary>
        /// 查询第一个章节
        /// </summary>
        /// <returns></returns>
        public async Task ReadChapterAsync()
        {
            Console.WriteLine(nameof(ReadChapterAsync));
            var chapters = await _client.GetAllAsync(_urlService.BooksApi);
            Guid id = chapters.First().Id;
            BookChapter chapter = await _client.GetAsync(_urlService.BooksApi + id);
            Console.WriteLine($"{chapter.Number} {chapter.Title}");
            Console.WriteLine();
        }
    }
}
