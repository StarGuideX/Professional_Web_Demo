using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIServiceSamples.BookServices.Models;

namespace WebAPIServiceSamples.BookServices.Services
{
    public class BookChaptersService : IBookChaptersService
    {
        // BookChaptersService作为单例注册，所以可以同时从多个线程访问它;
        // 这就是为什么在实现中需要ConcurrentDictionary的原因
        private readonly ConcurrentDictionary<Guid, BookChapter> _chapters = new ConcurrentDictionary<Guid, BookChapter>();

        #region sync
        public void Add(BookChapter bookChapter)
        {
            bookChapter.Id = Guid.NewGuid();
            _chapters[bookChapter.Id] = bookChapter;
        }

        public void AddRange(IEnumerable<BookChapter> chapters)
        {
            foreach (var chapter in chapters)
            {
                chapter.Id = Guid.NewGuid();
                _chapters[chapter.Id] = chapter;
            }
        }

        public BookChapter Find(Guid id)
        {
            _chapters.TryGetValue(id, out BookChapter chapter);
            return chapter;
        }

        public IEnumerable<BookChapter> GetAll() => _chapters.Values;

        public BookChapter Remove(Guid id)
        {
            _chapters.TryRemove(id, out BookChapter removed);
            return removed;
        }

        public void Update(BookChapter bookChapter)
        {
            _chapters[bookChapter.Id] = bookChapter;
        }
        #endregion
        #region async
        public Task AddAsync(BookChapter chapter)
        {
            chapter.Id = Guid.NewGuid();
            _chapters[chapter.Id] = chapter;
            return Task.CompletedTask;
        }

        public Task AddRangeAsync(IEnumerable<BookChapter> chapters)
        {
            foreach (var chapter in chapters)
            {
                chapter.Id = Guid.NewGuid();
                _chapters[chapter.Id] = chapter;
            }
            return Task.CompletedTask;
        }

        public Task<BookChapter> RemoveAsync(Guid id)
        {
            _chapters.TryRemove(id, out BookChapter removed);
            return Task.FromResult(removed);
        }

        public Task<IEnumerable<BookChapter>> GetAllAsync() =>
          Task.FromResult<IEnumerable<BookChapter>>(_chapters.Values);

        public Task<BookChapter> FindAsync(Guid id)
        {
            _chapters.TryGetValue(id, out BookChapter chapter);
            return Task.FromResult(chapter);
        }

        public Task UpdateAsync(BookChapter chapter)
        {
            _chapters[chapter.Id] = chapter;
            return Task.CompletedTask;
        }
        #endregion
    }
}