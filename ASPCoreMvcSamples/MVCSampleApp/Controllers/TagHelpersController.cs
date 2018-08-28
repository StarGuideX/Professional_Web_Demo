﻿using System;
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

    }
}