using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIOdataSamples.Models;

namespace WebAPIOdataSamples.Controllers
{
    public class BooksController: ODataController
    {
        private readonly BooksContext _booksContext;

        public BooksController(BooksContext booksContext)
        {
            _booksContext = booksContext ?? throw new ArgumentNullException(nameof(booksContext));
        }

        public IQueryable<Book> Get() => _booksContext.Books.Include(b => b.Chapters);

        //[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]    
        //public IQueryable<Book> Get(ODataQueryOptions options)
        //{
        //    ODataValidationSettings settings = new ODataValidationSettings()
        //    {
        //        MaxExpansionDepth = 4
        //    };
        //    options.Validate(settings);
        //    var books = _booksContext.Books.Include(b => b.Chapters);
        //    return books;
        //}

        [EnableQuery]
        public SingleResult<Book> Get([FromODataUri] int id) => 
            SingleResult.Create(_booksContext.Books.Where(b => b.Id.Equals(id)));
    }
}
