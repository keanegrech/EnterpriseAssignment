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

        public void DeleteAll()
        {
            _memoryCache.Remove("Resturants");
            _memoryCache.Remove("MenuItems");
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

        public IItemValidating Save(IItemValidating item)
        {
            if (item.GetType() == typeof(Resturant))
            {
                var resturants = _memoryCache.Get<List<Resturant>>("Resturants") ?? new List<Resturant>();
                var restaurant = (Resturant)item;
                restaurant.Id = resturants.Count > 0 ? resturants.Max(r => r.Id) + 1 : 1;
                resturants.Add(restaurant);
                _memoryCache.Set("Resturants", resturants);
            }
            else if (item.GetType() == typeof(MenuItem))
            {
                var menuItems = _memoryCache.Get<List<MenuItem>>("MenuItems") ?? new List<MenuItem>();
                var menuItem = (MenuItem)item;
                menuItem.Id = Guid.NewGuid();
                menuItems.Add(menuItem);
                _memoryCache.Set("MenuItems", menuItems);
            }

            return item;
        }
    }
}
