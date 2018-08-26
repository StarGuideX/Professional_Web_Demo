using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Models;

namespace MVCSampleApp.Controllers
{
    public class SubmitDataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateMenu() => View();

        [HttpPost]
        public IActionResult CreateMenu(int id, string text, double price, DateTime date, string category) {
            var m = new Menu
            {
                Id = id,
                Text = text,
                Price = price,
                Date = date,
                Category = category
            };
            ViewBag.Info = $"menu created: {m.Text}, Price: {m.Price}, " + $"date: {m.Date}, category: {m.Category}";
            return View("Index");
        }

        public IActionResult CreateMenu2() => View();

        [HttpPost]
        public IActionResult CreateMenu2(Menu menu)
        {
            ViewBag.Info = $"menu created: {menu.Text}, Price: { menu.Price}, " + $"date: {menu.Date}, category: {menu.Category}";
            return View("Index");
        }

        public IActionResult CreateMenu3() => View();

        /// <summary>
        /// 创建了新Menu类实例，
        /// 并将此实例传递给Controller基类的TryUpdateModelAsync方法。
        /// 如果更新后更新的模型未处于有效状态，则TryUpdateModelAsync返回false
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateMenu3Result()
        {
            //创建了新Menu类实例
            var m = new Menu();
            //并将此实例传递给Controller基类的TryUpdateModelAsync方法
            bool updated = await TryUpdateModelAsync<Menu>(m);
            //如果更新后更新的模型未处于有效状态，则TryUpdateModelAsync返回false：
            if (updated)
            {
                ViewBag.Info = $"menu created: {m.Text}, Price: {m.Price}, date: {m.Date.ToShortDateString()}, category: {m.Category}";
                return View("Index");
            }
            else
            {
                return View("Error");
            }
        }

        public IActionResult CreateMenu4() => View();
        [HttpPost]
        public IActionResult CreateMenu4(Menu menu)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Info =
                  $"menu created: {menu.Text}, Price: {menu.Price}, date: {menu.Date.ToShortDateString()}, category: {menu.Category}";
            }
            else
            {
                ViewBag.Info = "not valid";
            }
            return View("Index");
        }
    }
}