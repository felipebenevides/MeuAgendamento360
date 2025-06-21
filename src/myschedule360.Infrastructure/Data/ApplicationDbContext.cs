using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;
using myschedule360.Domain.ValueObjects;
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
        // Person configuration
        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("tb_persons");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100).HasColumnName("first_name");
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100).HasColumnName("last_name");
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255).HasColumnName("email");
            entity.Property(e => e.Phone).HasColumnName("phone")
                  .HasConversion(
                      v => v != null ? v.Value : null,
                      v => v != null ? new Phone(v) : null);
            entity.Property(e => e.CPF).HasColumnName("cpf")
                  .HasConversion(
                      v => v != null ? v.Value : null,
                      v => v != null ? new CPF(v) : null);
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Address configuration
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("tb_addresses");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Street).IsRequired().HasMaxLength(200).HasColumnName("street");
            entity.Property(e => e.Number).IsRequired().HasMaxLength(20).HasColumnName("number");
            entity.Property(e => e.Neighborhood).IsRequired().HasMaxLength(100).HasColumnName("neighborhood");
            entity.Property(e => e.City).IsRequired().HasMaxLength(100).HasColumnName("city");
            entity.Property(e => e.State).IsRequired().HasMaxLength(2).HasColumnName("state");
            entity.Property(e => e.Complement).HasColumnName("complement");
            entity.Property(e => e.CEP).HasColumnName("cep")
                  .HasConversion(
                      v => v.Value,
                      v => new CEP(v))
                  .IsRequired();
            entity.Property(e => e.Latitude).HasPrecision(10, 8).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasPrecision(11, 8).HasColumnName("longitude");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("tb_users");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255).HasColumnName("email");
            entity.Property(e => e.PasswordHash).IsRequired().HasColumnName("password_hash");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
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
            entity.ToTable("tb_businesses");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200).HasColumnName("name");
            entity.Property(e => e.Slug).IsRequired().HasMaxLength(100).HasColumnName("slug");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Website).HasColumnName("website");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.IsOnboardingComplete).HasColumnName("is_onboarding_complete");
            entity.Property(e => e.CNPJ).HasColumnName("cnpj")
                  .HasConversion(
                      v => v != null ? v.Value : null,
                      v => v != null ? new CNPJ(v) : null);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.HasIndex(e => e.Slug).IsUnique();
                      
            entity.HasOne(e => e.Address)
                  .WithMany()
                  .HasForeignKey(e => e.AddressId);
        });

        // Customer configuration
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("tb_customers");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            
            entity.HasOne(e => e.Person)
                  .WithOne(p => p.Customer)
                  .HasForeignKey<Customer>(e => e.PersonId);
        });

        // Service configuration
        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("tb_services");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Price).HasPrecision(10, 2).HasColumnName("price");
            entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        // Appointment configuration
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.ToTable("tb_appointments");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.ScheduledAt).HasColumnName("scheduled_at");
            entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
            entity.Property(e => e.Price).HasPrecision(10, 2).HasColumnName("price");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            
            entity.HasOne(e => e.Customer)
                  .WithMany(c => c.Appointments)
                  .HasForeignKey(e => e.CustomerId);
        });

        // Financial Transaction configuration
        modelBuilder.Entity<FinancialTransaction>(entity =>
        {
            entity.ToTable("tb_financial_transactions");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasPrecision(10, 2).HasColumnName("amount");
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500).HasColumnName("description");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.TransactionDate).HasColumnName("transaction_date");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
        });

        // Commission Rule configuration
        modelBuilder.Entity<CommissionRule>(entity =>
        {
            entity.ToTable("tb_commission_rules");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200).HasColumnName("name");
            entity.Property(e => e.Value).HasPrecision(10, 2).HasColumnName("value");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        // Staff configuration
        modelBuilder.Entity<Staff>(entity =>
        {
            entity.ToTable("tb_staff");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.CommissionRate).HasColumnName("commission_rate");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        // Staff Availability configuration
        modelBuilder.Entity<StaffAvailability>(entity =>
        {
            entity.ToTable("tb_staff_availabilities");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.DayOfWeek).IsRequired().HasColumnName("day_of_week");
            entity.Property(e => e.StartTime).IsRequired().HasColumnName("start_time");
            entity.Property(e => e.EndTime).IsRequired().HasColumnName("end_time");
            entity.Property(e => e.IsAvailable).HasColumnName("is_available");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            
            entity.HasOne(e => e.Staff)
                  .WithMany(s => s.Availabilities)
                  .HasForeignKey(e => e.StaffId);
        });

        // Inventory Item configuration
        modelBuilder.Entity<InventoryItem>(entity =>
        {
            entity.ToTable("tb_inventory_items");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Price).HasPrecision(10, 2).HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.MinimumStock).HasColumnName("minimum_stock");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            
            entity.HasOne(e => e.Business)
                  .WithMany()
                  .HasForeignKey(e => e.BusinessId);
        });
        
        // Notification configuration
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("tb_notifications");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200).HasColumnName("title");
            entity.Property(e => e.Message).IsRequired().HasColumnName("message");
            entity.Property(e => e.Type).HasMaxLength(50).HasColumnName("type");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.UserId).IsRequired().HasColumnName("user_id");
            entity.Property(e => e.Link).HasColumnName("link");
        });

        base.OnModelCreating(modelBuilder);
    }

    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var result = new StringBuilder();
        
        for (int i = 0; i < input.Length; i++)
        {
            if (char.IsUpper(input[i]))
            {
                if (i > 0)
                    result.Append('_');
                result.Append(char.ToLower(input[i]));
            }
            else
            {
                result.Append(input[i]);
            }
        }
        
        return result.ToString();
    }
}