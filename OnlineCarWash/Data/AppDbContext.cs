using Microsoft.EntityFrameworkCore;
using OnlineCarWash.Appointments.Models;
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
        public virtual DbSet<Appointment> Appointments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");
                entity.Property(s=>s.Email).IsRequired().HasMaxLength(256);
                entity.Property(s => s.NormalizedEmail).HasMaxLength(256);
                entity.Property(s => s.UserName).IsRequired().HasMaxLength(256);
                entity.Property(s => s.NormalizedUserName).HasMaxLength(256);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.PhoneNumber).IsRequired().HasMaxLength(256);

                entity.HasDiscriminator<string>("Discriminator").HasValue("Customer");

            });

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


            modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Service)
            .WithMany(a => a.Appointments)
            .HasForeignKey(a => a.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Customer)
            .WithMany(a => a.Appointments)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);


        }

    }
}
