using DataAccess.Context;
using System;
using System.Collections.Generic;
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
    }
}
