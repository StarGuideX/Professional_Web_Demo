using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Models;

namespace MVCSampleApp.Controllers
{
    public class ResultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ContentDemo() => Content("Hello World", "text/plain");

        public IActionResult JsonDemo()
        {
            var m = new Menu
            {
                Id = 3,
                Text = "鱼香肉丝",
                Price = 26.90,
                Date = new DateTime(2018, 3, 21),
                Category = "Main"
            };
            return Json(m);
        }

        public IActionResult RedirectDemo() => Redirect("https://www.cninnovation.com");

        public IActionResult RedirectRouteDemo() => RedirectToRoute(new { controller = "Home", action = "Hello" });
    }
}