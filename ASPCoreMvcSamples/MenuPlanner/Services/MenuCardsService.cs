using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace MenuPlanner.Services
{
    public class MenuCardsService : IMenuCardsService
    {
        private readonly MenuCardsContext _menuCardsContext;
        public MenuCardsService(MenuCardsContext menuCardsContext)
            => _menuCardsContext = menuCardsContext;

        public async Task AddMenuAsync(Menu menu)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMenuAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Menu> GetMenuByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MenuCard>> GetMenuCardsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Menu>> GetMenusAsync()
        {
            await EnsureDatabaseCreatedAsync();
            var menus = _menuCardsContext.Menus.Include(m => m.MenuCard);
            return menus.ToArrayAsync();
        }

        public Task UpdateMenuAsync(Menu menu)
        {
            throw new NotImplementedException();
        }

        private Task EnsureDatabaseCreatedAsync()
        {
            throw new NotImplementedException();
        }
    }
}
