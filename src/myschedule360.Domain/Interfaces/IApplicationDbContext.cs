using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;

namespace myschedule360.Domain.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Person> Persons { get; set; }
    DbSet<Address> Addresses { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Business> Businesses { get; set; }
    DbSet<Service> Services { get; set; }
    DbSet<Staff> Staff { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<Appointment> Appointments { get; set; }
    DbSet<FinancialTransaction> FinancialTransactions { get; set; }
    DbSet<InventoryItem> InventoryItems { get; set; }
    DbSet<CommissionRule> CommissionRules { get; set; }
    DbSet<StaffAvailability> StaffAvailabilities { get; set; }
    DbSet<Notification> Notifications { get; set; }
    DbSet<myschedule360.Domain.Entities.Task> Tasks { get; set; }
    DbSet<Store> Stores { get; set; }
    DbSet<Owner> Owners { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}