namespace myschedule360.Domain.Entities;

public class Customer
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public Person Person { get; set; } = null!;
    public string? Notes { get; set; }
    public Guid BusinessId { get; set; }
    public Business Business { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}