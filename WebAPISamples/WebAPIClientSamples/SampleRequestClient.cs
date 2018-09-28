using System;
using System.Collections.Generic;
using System.Net.Http;
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

        public async  Task ReadChaptersAsync()
        {
            Console.WriteLine(nameof(ReadChaptersAsync));
            IEnumerable<BookChapter> chapters = await _client.GetAllAsync(_urlService.BooksApi);
            foreach (BookChapter chapter in chapters)
            {
                Console.WriteLine(chapter.Title);
            }
        }

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

        public Task UpdateChapterAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveChapterAsync()
        {
            throw new NotImplementedException();
        }

        public Task AddChapterAsync()
        {
            throw new NotImplementedException();
        }

        public Task ReadXmlAsync()
        {
            Console.WriteLine(nameof(ReadXmlAsync));
            XElement chapters = await _client.GetAsync

        }

        public Task ReadChapterAsync()
        {
            throw new NotImplementedException();
        }
    }
}
