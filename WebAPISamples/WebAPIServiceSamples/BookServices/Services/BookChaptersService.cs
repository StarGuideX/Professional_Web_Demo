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
        private readonly ConcurrentDictionary<Guid, BookChapter> _chatpers = new ConcurrentDictionary<Guid, BookChapter>();
        public void Add(BookChapter bookChapter)
        {
            bookChapter.Id = Guid.NewGuid();
            _chatpers[bookChapter.Id] = bookChapter;
        }

        public void AddRange(IEnumerable<BookChapter> chapters)
        {
            foreach (var chapter in chapters)
            {
                chapter.Id = Guid.NewGuid();
                _chatpers[chapter.Id] = chapter;
            }
        }

        public BookChapter Find(Guid id)
        {
            _chatpers.TryGetValue(id, out BookChapter chapter);
            return chapter;
        }

        public IEnumerable<BookChapter> GetAll() => _chatpers.Values;

        public BookChapter Remove(Guid id)
        {
            _chatpers.TryRemove(id, out BookChapter removed);
            return removed;
        }

        public void Update(BookChapter bookChapter)
        {
            _chatpers[bookChapter.Id] = bookChapter;
        }
    }
}