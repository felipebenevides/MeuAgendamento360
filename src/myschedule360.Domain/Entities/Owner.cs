namespace myschedule360.Domain.Entities;

public class Owner
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    public ICollection<Business> Businesses { get; set; } = new List<Business>();
    public ICollection<Store> Stores { get; set; } = new List<Store>();
}