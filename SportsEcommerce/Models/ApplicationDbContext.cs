using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsEcommerce.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base (opts){}

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
