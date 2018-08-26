using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Models;

namespace MVCSampleApp.Controllers
{
    public class ViewsDemoController : Controller
    {
        private EventsAndMenusContext _context;

        /// <summary>
        /// 依赖注入EventsAndMenusContext
        /// </summary>
        /// <param name="context"></param>
        public ViewsDemoController(EventsAndMenusContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PassingData()
        {
            ViewBag.MyData = "从控制器传递了数据";
            return View();
        }

        public IActionResult PassingAModel() => View(GetSampleData());

        private IEnumerable<Menu> GetSampleData() =>
            new List<Menu>
            {
                new Menu
                {
                    Id = 1,
                    Text = "宫保鸡丁",
                    Price = 6.9,
                    Category = "Main"
                },
                new Menu
                {
                    Id = 2,
                    Text = "水煮牛肉",
                    Price = 6.9,
                    Category = "Main"
                },
                 new Menu
                {
                    Id = 3,
                    Text = "辣炒蛤蜊",
                    Price = 6.9,
                    Category = "Main"
                }
            };

        public IActionResult LayoutSample() => View();

        public IActionResult LayoutUsingSections() => View();

        public IActionResult UseAPartialView1() => View(_context);
        public ActionResult UseAPartialView2() => View();
        public ActionResult ShowEvents()
        {
            ViewBag.EventsTitle = "Live Events";
            return PartialView(_context.Events);
        }

        public IActionResult UseViewComponent1() => View();
        public IActionResult UseViewComponent2() => View();
        public IActionResult InjectServiceInView() => View();
    }
}