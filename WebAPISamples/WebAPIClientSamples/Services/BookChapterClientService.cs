using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAPIClientSamples.Models;
using System.Linq;

namespace WebAPIClientSamples.Services
{
    public class BookChapterClientService : HttpClientService<BookChapter>
    {
        public BookChapterClientService
            (UrlService urlService, ILogger<BookChapterClientService> logger) : 
            base(urlService, logger) { }

        public override async Task<IEnumerable<BookChapter>> GetAllAsync(string requestUri)
        {
            IEnumerable<BookChapter> chapters = await base.GetAllAsync(requestUri);

            return chapters.OrderBy(c => c.Number);
        }
    }
}
