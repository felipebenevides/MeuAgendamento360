namespace myschedule360.Domain.Entities;

public class StaffAvailability
{
    public Guid Id { get; set; }
    public Guid StaffId { get; set; }
    public Staff Staff { get; set; } = null!;
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}