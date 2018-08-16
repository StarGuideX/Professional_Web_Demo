﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCSampleApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string Hello() => "Hello, ASP.NET Core MVC";

        public string Greeting(string name) => HtmlEncoder.Default.Encode($"Hello,{name}");

        public string Greeting2(string id) => HtmlEncoder.Default.Encode($"Hello, {id}");

        public int Add(int x, int y) => x + y;
    }
}