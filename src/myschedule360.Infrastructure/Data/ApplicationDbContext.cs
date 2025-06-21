using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;
using myschedule360.Domain.ValueObjects;
using myschedule360.Infrastructure.Data;
using System.Text;

namespace myschedule360.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Business> Businesses { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Staff> Staff { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
    public DbSet<InventoryItem> InventoryItems { get; set; }
    public DbSet<CommissionRule> CommissionRules { get; set; }
    public DbSet<StaffAvailability> StaffAvailabilities { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure entity properties with specific requirements
        ConfigureEntityProperties(modelBuilder);
        
        // Apply snake_case naming convention to all tables and columns
        modelBuilder.ApplySnakeCaseNamingConvention();
        
        base.OnModelCreating(modelBuilder);
    }
    
    private void ConfigureEntityProperties(ModelBuilder modelBuilder)
    {
        // Person configuration
        modelBuilder.Entity<Person>(entity =>
        {
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Phone)
                  .HasConversion(
                      v => v != null ? v.Value : null,
                      v => v != null ? new Phone(v) : null);
            entity.Property(e => e.CPF)
                  .HasConversion(
                      v => v != null ? v.Value : null,
                      v => v != null ? new CPF(v) : null);
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Address configuration
        modelBuilder.Entity<Address>(entity =>
        {
            entity.Property(e => e.Street).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Number).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Neighborhood).IsRequired().HasMaxLength(100);
            entity.Property(e => e.City).IsRequired().HasMaxLength(100);
            entity.Property(e => e.State).IsRequired().HasMaxLength(2);
            entity.Property(e => e.CEP)
                  .HasConversion(
                      v => v.Value,
                      v => new CEP(v))
                  .IsRequired();
            entity.Property(e => e.Latitude).HasPrecision(10, 8);
            entity.Property(e => e.Longitude).HasPrecision(11, 8);
        });

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
            
            entity.HasOne(e => e.Person)
                  .WithOne(p => p.User)
                  .HasForeignKey<User>(e => e.PersonId);
                  
            entity.HasOne(e => e.Business)
                  .WithMany(b => b.Users)
                  .HasForeignKey(e => e.BusinessId);
        });

        // Business configuration
        modelBuilder.Entity<Business>(entity =>
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Slug).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CNPJ)
                  .HasConversion(
                      v => v != null ? v.Value : null,
                      v => v != null ? new CNPJ(v) : null);
            entity.HasIndex(e => e.Slug).IsUnique();
                      
            entity.HasOne(e => e.Address)
                  .WithMany()
                  .HasForeignKey(e => e.AddressId);
        });

        // Customer configuration
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasOne(e => e.Person)
                  .WithOne(p => p.Customer)
                  .HasForeignKey<Customer>(e => e.PersonId);
        });

        // Service configuration
        modelBuilder.Entity<Service>(entity =>
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Price).HasPrecision(10, 2);
        });

        // Appointment configuration
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.Property(e => e.Price).HasPrecision(10, 2);
            
            entity.HasOne(e => e.Customer)
                  .WithMany(c => c.Appointments)
                  .HasForeignKey(e => e.CustomerId);
        });

        // Financial Transaction configuration
        modelBuilder.Entity<FinancialTransaction>(entity =>
        {
            entity.Property(e => e.Amount).HasPrecision(10, 2);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
        });

        // Commission Rule configuration
        modelBuilder.Entity<CommissionRule>(entity =>
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Value).HasPrecision(10, 2);
        });

        // Staff Availability configuration
        modelBuilder.Entity<StaffAvailability>(entity =>
        {
            entity.Property(e => e.DayOfWeek).IsRequired();
            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.EndTime).IsRequired();
            
            entity.HasOne(e => e.Staff)
                  .WithMany(s => s.Availabilities)
                  .HasForeignKey(e => e.StaffId);
        });

        // Inventory Item configuration
        modelBuilder.Entity<InventoryItem>(entity =>
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Price).HasPrecision(10, 2);
            
            entity.HasOne(e => e.Business)
                  .WithMany()
                  .HasForeignKey(e => e.BusinessId);
        });
        
        // Notification configuration
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Message).IsRequired();
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UserId).IsRequired();
        });
    }
    }


}