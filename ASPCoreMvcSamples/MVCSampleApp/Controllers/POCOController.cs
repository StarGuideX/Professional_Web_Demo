﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSampleApp.Controllers
{
    public class POCOController
    {
        public string Index() => "这是一个POCOController";

        /// <summary>
        /// POCOController类使用ActionContext作为此属性的名称，就像Controller类一样。
        /// </summary>
        [ActionContext]
        public ActionContext ActionContext { get; set; }

        public HttpContext HttpContext => ActionContext.HttpContext;
        public ModelStateDictionary ModelState => ActionContext.ModelState;

        public string UserAgentInfo()
        {
            if (HttpContext.Request.Headers.ContainsKey("User-Agent"))
            {
                return HttpContext.Request.Headers["User-Agent"];
            }
            return "No user-agent information";
        }
    }
}
