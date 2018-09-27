using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIServiceSamples.BookServices.Models;

namespace WebAPIServiceSamples.BookServices.Services
{
    public class SampleChapters
    {
        private readonly IBookChaptersService _bookChaptersService;

        public SampleChapters(IBookChaptersService bookChaptersService)
        {
            _bookChaptersService = bookChaptersService;
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

        private int[] chapterNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 44 };
        private int[] numberPages = { 35, 42, 33, 20, 24, 38, 20, 32, 44 };

        /// <summary>
        /// 初始化，填充数据
        /// </summary>
        public void CreateSampleChapters()
        {
            var chapters = new List<BookChapter>();
            for (int i = 0; i < 8; i++)
            {
                chapters.Add(new BookChapter
                {
                    Number = chapterNumbers[i],
                    Title = sampleTitles[i],
                    Pages = numberPages[i]
                });
            }
            _bookChaptersService.AddRange(chapters);
        }
    }
}
