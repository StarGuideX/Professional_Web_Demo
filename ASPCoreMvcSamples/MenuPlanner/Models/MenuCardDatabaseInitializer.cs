using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuPlanner.Models
{
public class MenuCardDatabaseInitializer
{
    private static bool s_databaseChecked = false;
    private readonly MenuCardsContext _context;

    public MenuCardDatabaseInitializer(MenuCardsContext context)
        => _context = context;

    public async Task CreateAndSeedDatabaseAsync()
    {
        if (! s_databaseChecked)
        {
            s_databaseChecked = true;
            //启动迁移
            await _context.Database.MigrateAsync();
            //MigrateAsync会检查—连接字符串关联的数据库和迁移中制定的数据库是否是相同的版本，如果不同，则调用Up方法获得相同的版本

            //如果无数据，填充数据库
            if (await _context.MenuCards.CountAsync() == 0)
            {
                var menuCards = new List<MenuCard>() {
                    new MenuCard { Name = "Breakfast", Active = true,Order = 1 },
                    new MenuCard { Name = "Vegetarian", Active =true, Order = 2 },
                    new MenuCard { Name = "Steaks", Active = true, Order = 3 }
                };
                _context.MenuCards.AddRange(menuCards);
                await _context.SaveChangesAsync();
            }
        }
    }
}
}
