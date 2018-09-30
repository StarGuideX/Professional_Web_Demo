using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAPIServiceSamples.BookServices.Models;

namespace WebAPIServiceSamples.BookServices.Services
{
    public class DBBookChaptersService : IBookChaptersService
    {
        private readonly BooksContext _booksContext;

        public DBBookChaptersService(BooksContext booksContext)
        {
            this._booksContext = booksContext ?? throw new ArgumentNullException(nameof(booksContext));

            bool created = _booksContext.Database.EnsureCreated();
            if (created)
            {
                CreateChapters();
            }

        }

        private string[] sampleTitles = new[]
       {
            "BookTitles1",
            "BookTitles2",
            "BookTitles3",
            "BookTitles4",
            "BookTitles5",
            "BookTitles6",
            "BookTitles7",
            "BookTitles8",
            "BookTitles9",
        };

        private int[] numberPages = { 35, 42, 33, 20, 24, 38, 20, 32, 44 };

        /// <summary>
        /// 初始化，填充数据
        /// </summary>
        public void CreateChapters()
        {
            var chapters = new List<BookChapter>();
            for (int i = 0; i < 8; i++)
            {
                chapters.Add(new BookChapter
                {
                    Number = i,
                    Title = sampleTitles[i],
                    Pages = numberPages[i]
                });
            }
            _booksContext.AddRange(chapters);
            _booksContext.SaveChanges();
        }

        public void Add(BookChapter bookChapter)
        {
            _booksContext.Chapters.Add(bookChapter);
            _booksContext.SaveChanges();
        }

        public async Task AddAsync(BookChapter chapter)
        {
            await _booksContext.Chapters.AddAsync(chapter);
            await _booksContext.SaveChangesAsync();
        }

        public void AddRange(IEnumerable<BookChapter> chapters)
        {
            _booksContext.Chapters.AddRange(chapters);
            _booksContext.SaveChanges();
        }

        public async Task AddRangeAsync(IEnumerable<BookChapter> chapters)
        {
            await _booksContext.Chapters.AddRangeAsync(chapters);
            await _booksContext.SaveChangesAsync();
        }

        public BookChapter Find(Guid id) => _booksContext.Chapters.Find(id);

        public Task<BookChapter> FindAsync(Guid id) => _booksContext.Chapters.FindAsync(id);

        public IEnumerable<BookChapter> GetAll() => _booksContext.Chapters;

        public async Task<IEnumerable<BookChapter>> GetAllAsync() => await _booksContext.Chapters.ToListAsync();

        public BookChapter Remove(Guid id)
        {
            var chapter = _booksContext.Chapters.Find(id);
            if (chapter != null)
            {
                _booksContext.Chapters.Remove(chapter);
                _booksContext.SaveChanges();
            }
            return chapter;
        }


        public async Task<BookChapter> RemoveAsync(Guid id)
        {
            var chapter = await _booksContext.Chapters.FindAsync(id);
            if (chapter != null)
            {
                _booksContext.Chapters.Remove(chapter);
                await _booksContext.SaveChangesAsync();
            }
            return chapter;
        }

        public void Update(BookChapter bookChapter)
        {
            _booksContext.Chapters.Update(bookChapter);
            _booksContext.SaveChanges();
        }

        public async Task UpdateAsync(BookChapter chapter)
        {
            _booksContext.Chapters.Update(chapter);
            await _booksContext.SaveChangesAsync();
        }
    }
}
