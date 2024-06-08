using Microsoft.EntityFrameworkCore;
using OnlineCarWash.Customers.Models;
using OnlineCarWash.Options.Models;
using OnlineCarWash.Services.Models;
using OnlineCarWash.ServicesOptions.Models;

namespace OnlineCarWash.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        
        
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<ServiceOption> ServiceOptions { get; set; }
        public virtual DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceOption>()
            .HasOne(a => a.Option)
            .WithMany(a => a.Services)
            .HasForeignKey(a => a.OptionId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServiceOption>()
            .HasOne(a => a.Service)
            .WithMany(a => a.Options)
            .HasForeignKey(a => a.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
