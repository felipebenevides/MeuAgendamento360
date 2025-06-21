namespace myschedule360.Domain.Entities;

public class Staff
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid BusinessId { get; set; }
    public Business Business { get; set; } = null!;
    public string Role { get; set; } = string.Empty;
    public decimal? CommissionRate { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<StaffAvailability> Availabilities { get; set; } = new List<StaffAvailability>();
}