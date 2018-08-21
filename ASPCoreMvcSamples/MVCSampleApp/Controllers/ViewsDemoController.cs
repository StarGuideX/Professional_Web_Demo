using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCSampleApp.Controllers
{
    public class ViewsDemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PassingData()
        {
            ViewBag.MyData = "从控制器传递了数据";
            return View();
        }

        
    }
}