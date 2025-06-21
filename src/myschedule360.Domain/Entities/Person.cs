using myschedule360.Domain.ValueObjects;

namespace myschedule360.Domain.Entities;

public class Person
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Phone? Phone { get; set; }
    public CPF? CPF { get; set; }
    public DateTime? BirthDate { get; set; }
    public PersonType Type { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    public User? User { get; set; }
    public Customer? Customer { get; set; }
}

public enum PersonType
{
    BusinessOwner = 1,
    ServiceProvider = 2,
    Customer = 3
}