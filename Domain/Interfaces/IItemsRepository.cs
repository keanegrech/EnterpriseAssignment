using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IItemsRepository
    {
        IQueryable<Resturant> GetResturants();
        IQueryable<MenuItem> GetMenuItems();
        IItemValidating Save(IItemValidating item);

        void ApproveResturant(int id);
        void ApproveMenuItem(Guid id);

        void DeleteAll();
    }
}
