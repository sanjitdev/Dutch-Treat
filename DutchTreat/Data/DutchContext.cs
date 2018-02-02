using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace DutchTreat.Data
{
    public class DutchContext: IdentityDbContext<StoreUser>
    {
        public DutchContext(DbContextOptions<DutchContext> options):base(options)
        {

        }
        public DbSet<Product> Products{get;set;}
        public DbSet<Order> Orders{get;set;}
    }
}