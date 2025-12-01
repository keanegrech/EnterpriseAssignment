using DataAccess.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class MenuItemRepository
    {
        private ResturantMenuItemDbContext _context;
        public MenuItemRepository(ResturantMenuItemDbContext context)
        {
            _context = context;
        }

        public IQueryable<MenuItem> Get()
        {
            return _context.MenuItems;
        }
        public MenuItem Get(Guid id)
        {
            return _context.MenuItems.SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<MenuItem> Get(string keyword)
        {
            return Get().Where(x => x.Title.Contains(keyword));
        }

        public void Add(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            _context.SaveChanges();
        }

        public void Update(MenuItem menuItem)
        {
            var original = Get(menuItem.Id);

            if (original != null)
            {
                original.Title = menuItem.Title;
                original.Price = menuItem.Price;
                original.Status = menuItem.Status;
                original.ResturantFK = menuItem.ResturantFK;

                _context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            var menuItem = Get(id);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
                _context.SaveChanges();
            }
        }
    }
}
