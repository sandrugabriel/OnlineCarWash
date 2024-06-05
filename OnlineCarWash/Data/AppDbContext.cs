using Microsoft.EntityFrameworkCore;
using OnlineCarWash.Customers.Models;

namespace OnlineCarWash.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        
        
        }

        public virtual DbSet<Customer> Customers { get; set; }



    }
}
