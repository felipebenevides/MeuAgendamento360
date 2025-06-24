namespace myschedule360.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public Guid? StoreId { get; set; }
    public Guid? BusinessId { get; set; }
    public Store? Store { get; set; }
    public Business? Business { get; set; }
    public Guid PersonId { get; set; }
    public Person Person { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    public Owner? Owner { get; set; }
}

public enum UserRole
{
    BusinessOwner = 1,
    ServiceProvider = 2
}