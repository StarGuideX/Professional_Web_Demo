using Microsoft.AspNet.OData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIOdataSamples.Models;

namespace WebAPIOdataSamples.Controllers
{
    public class ChaptersController: ODataController
    {
        private readonly BooksContext _booksContext;
        public ChaptersController(BooksContext booksContext)
        {
            _booksContext = booksContext;
        }

        public IQueryable<BookChapter> Get() => _booksContext.Chapters.Include(c => c.Book);
        [EnableQuery]
        public SingleResult<BookChapter> Get([FromODataUri] int  key) => 
            SingleResult.Create(_booksContext.Chapters.Where(c => c.Id == key));
    }
}
