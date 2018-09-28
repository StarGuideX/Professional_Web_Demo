using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIServiceSamples.BookServices.Models;

namespace WebAPIServiceSamples.BookServices.Services
{
    public interface IBookChaptersService
    {
        #region sync
        void Add(BookChapter bookChapter);
        void AddRange(IEnumerable<BookChapter> chapters);
        IEnumerable<BookChapter> GetAll();
        BookChapter Find(Guid id);
        BookChapter Remove(Guid id);
        void Update(BookChapter bookChapter);
        #endregion

        #region async
        Task AddAsync(BookChapter chapter);
        Task AddRangeAsync(IEnumerable<BookChapter> chapters);
        Task<BookChapter> RemoveAsync(Guid id);
        Task<IEnumerable<BookChapter>> GetAllAsync();
        Task<BookChapter> FindAsync(Guid id);
        Task UpdateAsync(BookChapter chapter);
        #endregion

    }
}
