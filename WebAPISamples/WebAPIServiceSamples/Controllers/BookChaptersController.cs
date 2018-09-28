using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIServiceSamples.BookServices.Models;
using WebAPIServiceSamples.BookServices.Services;

namespace WebAPIServiceSamples.Controllers
{
    //改变响应格式
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    // [ApiController]
    public class BookChaptersController : Controller
    {
        private readonly IBookChaptersService _bookChaptersService;
        #region sync
        public BookChaptersController(IBookChaptersService bookChaptersService)
        {
            _bookChaptersService = bookChaptersService;
        }

        // GET api/bookchapters
        /// <summary>
        /// 获取所有BookChapters
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<BookChapter> GetBookChapters() => _bookChaptersService.GetAll();

        // GET api/bookchapters/guid
        /// <summary>
        /// 根据ID查找BookChapter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = nameof(GetBookChapterById))]
        public IActionResult GetBookChapterById(Guid id)
        {
            BookChapter chapter = _bookChaptersService.Find(id);
            if (chapter == null)
            {
                // NotFound返回404（未找到）响应
                return NotFound();
            }
            else
            {
                // ObjectResult返回状态代码200，其中包含正文中的书籍章节
                return new ObjectResult(chapter);
            }
        }

        // POST api/bookchapters
        /// <summary>
        /// PostBookChapter接收作为HTTP主体的一部分的BookChapter，在反序列化后分配给PostBookChapter方法参数。
        /// </summary>
        /// <param name="chapter"></param>
        /// <returns></returns>
        public IActionResult PostBookChapter([FromBody]BookChapter chapter)
        {
            //如果参数BookChapter为空，则返回BadRequest（HTTP错误400）
            if (chapter == null)
            {
                return BadRequest();
            }
            _bookChaptersService.Add(chapter);
            // CreatedAtRoute返回HTTP状态201（已创建），并且对象已序列化。
            // 返回的头信息包含指向资源的链接，即指向GetBookChapterById的链接，其id设置为新创建的对象的标识符
            return CreatedAtRoute(nameof(GetBookChapterById), new { id = chapter.Id }, chapter);
        }

        // PUT api/bookchapters/guid
        /// <summary>
        /// 更新项目是基于HTTP PUT请求。 PutBookChapter方法更新集合中的现有项。  
        /// </summary>
        /// <param name="id"></param>
        /// <param name="chapter"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult PutBookChapter(Guid id, [FromBody]BookChapter chapter)
        {
            // 如果参数BookChapter或者id为空，则返回BadRequest（HTTP错误400）
            if (chapter == null || id != chapter.Id)
            {
                return BadRequest();
            }
            // 如果对象尚未在集合中，则返回NotFound。
            if (_bookChaptersService.Find(id) == null)
            {
                return NotFound();
            }
            // 如果找到该对象，则更新该对象，并且返回成功结果204 - 空主体的内容(NoContentResult)
            _bookChaptersService.Update(chapter);
            return new NoContentResult();
        }

        // DELETE api/bookchapters/5
        /// <summary>
        /// 删除Bookchapters
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(Guid id) => _bookChaptersService.Remove(id);
        #endregion


        #region async
        // GET: api/bookchapters
        [HttpGet()]
        public Task<IEnumerable<BookChapter>> GetBookChaptersAsync() => _bookChaptersService.GetAllAsync();
        // GET api/bookchapters/guid
        [HttpGet("{id}", Name = nameof(GetBookChapterByIdAsync))]
        public async Task<IActionResult> GetBookChapterByIdAsync(Guid id)
        {
            BookChapter chapter = await _bookChaptersService.FindAsync(id);
            if (chapter == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(chapter);
            }
        }
        // POST api/bookchapters
        [HttpPost]
        public async Task<IActionResult> PostBookChapterAsync([FromBody]BookChapter chapter)
        {
            if (chapter == null)
            {
                return BadRequest();
            }
            await _bookChaptersService.AddAsync(chapter);
            return CreatedAtRoute(nameof(GetBookChapterByIdAsync),
            new { id = chapter.Id }, chapter);
        }
        // PUT api/bookchapters/guid
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookChapterAsync(Guid id, [FromBody]BookChapter chapter)
        {
            if (chapter == null || id != chapter.Id)
            {
                return BadRequest();
            }
            if (await _bookChaptersService.FindAsync(id) == null)
            {
                return NotFound();
            }
            await _bookChaptersService.UpdateAsync(chapter);
            return new NoContentResult();
        }
        // DELETE api/bookchapters/guid
        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id) => await _bookChaptersService.RemoveAsync(id);
        #endregion
    }
}