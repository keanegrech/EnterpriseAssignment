using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class ResturantMenuItemDbContext : DbContext
    {
        public ResturantMenuItemDbContext(DbContextOptions<ResturantMenuItemDbContext> options) : base(options) { }

        public DbSet<Resturant> Resturants { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
