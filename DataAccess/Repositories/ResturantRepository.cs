using DataAccess.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ResturantRepository
    {
        private ResturantMenuItemDbContext _context;
        public ResturantRepository(ResturantMenuItemDbContext context)
        {
            _context = context;
        }

        public IQueryable<Resturant> Get()
        {
            return _context.Resturants;
        }

        public Resturant Get(int id)
        {
            return _context.Resturants.SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<Resturant> Get(string keyword)
        {
            return Get().Where(x => x.Name.Contains(keyword));
        }

        public void Add(Resturant resturant)
        {
            _context.Resturants.Add(resturant);
            _context.SaveChanges();
        }

        public void Update(Resturant resturant)
        {
            var original = Get(resturant.Id);

            if (original != null)
            {
                original.Name = resturant.Name;
                original.Description = resturant.Description;
                original.OwnerEmailAddress = resturant.OwnerEmailAddress;
                original.Address = resturant.Address;
                original.Status = resturant.Status;

                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var resturant = Get(id);
            if (resturant != null)
            {
                _context.Resturants.Remove(resturant);
                _context.SaveChanges();
            }
        }
    }
}
