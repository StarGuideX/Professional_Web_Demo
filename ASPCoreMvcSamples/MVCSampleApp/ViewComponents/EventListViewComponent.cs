using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSampleApp.ViewComponents
{
    public class EventListViewComponent : ViewComponent
    {
        //需要在Startup中注册的EventsAndMenusContext类型。
        private readonly EventsAndMenusContext _context;
        public EventListViewComponent(EventsAndMenusContext context)
        {
            _context = context;
        }
        /// <summary>
        /// InvokeAsync方法定义为从显示ViewComponent的View中调用。
        /// 此方法可以包含任何数量和类型的参数，因为IViewComponentHelper接口定义的方法使用params关键字定义了灵活数量的参数。
        /// 也可以使用同步实现此方法，返回的是IViewComponentResult。
        /// 最好使用异步，例如访问数据库。
        /// view component需要存储在ViewComponents目录中。 该目录本身可以放在项目中的任何位置
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public Task<IViewComponentResult> InvokeAsync(DateTime from, DateTime to) =>
            Task.FromResult<IViewComponentResult>(View(EventsByDateRange(from, to)));
        private IEnumerable<Event> EventsByDateRange(DateTime from, DateTime to) =>
            _context.Events.Where(e => e.Day >= from && e.Day <= to);
    }
}
