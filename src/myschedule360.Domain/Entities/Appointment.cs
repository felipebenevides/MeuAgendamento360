namespace myschedule360.Domain.Entities;

public class Appointment
{
    public Guid Id { get; set; }
    public DateTime ScheduledAt { get; set; }
    public int DurationMinutes { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
    public decimal Price { get; set; }
    public string? Notes { get; set; }
    
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    
    public Guid ServiceId { get; set; }
    public Service Service { get; set; } = null!;
    
    public Guid StaffId { get; set; }
    public Staff Staff { get; set; } = null!;
    
    public Guid BusinessId { get; set; }
    public Business Business { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public enum AppointmentStatus
{
    Scheduled = 1,
    Confirmed = 2,
    InProgress = 3,
    Completed = 4,
    Cancelled = 5,
    NoShow = 6
}