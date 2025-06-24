using myschedule360.Domain.ValueObjects;

namespace myschedule360.Domain.Entities;

public class Store
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public CNPJ? CNPJ { get; set; }
    public string? Website { get; set; }
    public BusinessType Type { get; set; }
    public bool IsOnboardingComplete { get; set; } = false;
    public Guid? AddressId { get; set; }
    public Address? Address { get; set; }
    public Guid OwnerId { get; set; }
    public Owner Owner { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Service> Services { get; set; } = new List<Service>();
    public ICollection<Staff> Staff { get; set; } = new List<Staff>();
    public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
    public ICollection<FinancialTransaction> FinancialTransactions { get; set; } = new List<FinancialTransaction>();
}