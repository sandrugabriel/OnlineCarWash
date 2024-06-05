using Microsoft.EntityFrameworkCore;
using OnlineCarWash.Customers.Models;
using OnlineCarWash.Options.Models;

namespace OnlineCarWash.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        
        
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Option> Options { get; set; }



    }
}
