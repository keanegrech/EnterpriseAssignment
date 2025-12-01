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

        public IQueryable<MenuItem> GetMenuItems()
        {
            return _context.MenuItems;
        }

        public IQueryable<Resturant> GetResturants()
        {
            return _context.Resturants;
        }

        public void Save(IItemValidating item)
        {
            if (item.GetType() == typeof(Resturant))
            {
                _context.Resturants.Add((Resturant)item);
            }
            else if (item.GetType() == typeof(MenuItem))
            {
                _context.MenuItems.Add((MenuItem)item);
            }
        }
    }
}
