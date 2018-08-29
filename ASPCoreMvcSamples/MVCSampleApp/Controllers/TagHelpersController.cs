using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Models;

namespace MVCSampleApp.Controllers
{
    public class TagHelpersController : Controller
    {
        private Menu GetSampleMenu() =>
            new Menu
            {
                Id = 1,
                Text = "Schweinsbraten mit Knödel und Sauerkraut",
                Price = 6.9,
                Date = new DateTime(2018, 10, 5),
                Category = "Main"
            };
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LabelHelper() => View(GetSampleMenu());
        public IActionResult InputHelper() => View(GetSampleMenu());
        public IActionResult FormHelper() => View(GetSampleMenu());
        [HttpPost]
        public IActionResult FormHelper(Menu m)
        {
            if (!ModelState.IsValid)
            {
                return View(m);
            }

            return View("ValidationHelperResult", m);
        }
        public IActionResult EnvironmentHelper() => View(GetSampleMenu());

        public IActionResult Markdown() => View();

        public IActionResult MarkdownAttribute() => View();

        public IActionResult CustomTable() => View(GetSampleMenus());

        private IList<Menu> GetSampleMenus() =>
            new List<Menu>
            {
                new Menu
                {
                    Id = 1,
                    Text = "Schweinsbraten mit Knödel und Sauerkraut",
                    Price = 8.5,
                    Date = new DateTime(2018, 10, 5),
                    Category = "Main"
                },
                new Menu
                {
                    Id = 2,
                    Text = "Erdäpfelgulasch mit Tofu und Gebäck",
                    Price = 8.5,
                    Date = new DateTime(2018, 10, 6),
                    Category = "Vegetarian"
                },
                new Menu
                {
                    Id = 3,
                    Text = "Tiroler Bauerngröst'l mit Spiegelei und Krautsalat",
                    Price = 8.5,
                    Date = new DateTime(2018, 10, 7),
                    Category = "Vegetarian"
                }
            };
    }
}