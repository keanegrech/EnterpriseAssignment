using DataAccess.Context;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ItemsDbRepository : IItemsRepository
    {
        private ResturantMenuItemDbContext _context;

        public ItemsDbRepository(ResturantMenuItemDbContext context)
        {
            _context = context;
        }

        public void DeleteAll()
        {
            throw new NotSupportedException();
        }

        public IQueryable<MenuItem> GetMenuItems()
        {
            return _context.MenuItems;
        }

        public IQueryable<Resturant> GetResturants()
        {
            return _context.Resturants;
        }

        public IItemValidating Save(IItemValidating item)
        {
            if (item.GetType() == typeof(Resturant))
            {
                _context.Resturants.Add((Resturant)item);
                _context.SaveChanges();
                return item;
            }
            else if (item.GetType() == typeof(MenuItem))
            {
                _context.MenuItems.Add((MenuItem)item);
                _context.SaveChanges();
                return item;
            }

            return null;
        }

        public void ApproveResturant(int id)
        {
            var resturant = GetResturants().FirstOrDefault(r => r.Id == id);
            resturant.Status = "approved";
            _context.SaveChanges();
        }

        public void ApproveMenuItem(Guid id)
        {
            var menuItem = GetMenuItems().FirstOrDefault(m => m.Id == id);
            menuItem.Status = "approved";
            _context.SaveChanges();
        }
    }
}
