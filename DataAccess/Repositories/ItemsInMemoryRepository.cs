using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ItemsInMemoryRepository : IItemsRepository
    {

        private IMemoryCache _memoryCache;

        public ItemsInMemoryRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IQueryable<MenuItem> GetMenuItems()
        {
            var menuItems = _memoryCache.Get<List<MenuItem>>("MenuItems");
            return menuItems.AsQueryable();
        }

        public IQueryable<Resturant> GetResturants()
        {
            var resturants = _memoryCache.Get<List<Resturant>>("Resturants");
            return resturants.AsQueryable();
        }

        public void Save(IItemValidating item)
        {
            if (item.GetType() == typeof(Resturant))
            {
                _memoryCache.Set("Resturants", (Resturant)item);
            }
            else if (item.GetType() == typeof(MenuItem))
            {
                _memoryCache.Set("MenuItems", (MenuItem)item);
            }
        }
    }
}
