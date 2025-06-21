namespace myschedule360.Domain.Entities;

public class CommissionRule
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public CommissionType Type { get; set; }
    public decimal Value { get; set; }
    public Guid? ServiceId { get; set; }
    public Service? Service { get; set; }
    public Guid? StaffId { get; set; }
    public Staff? Staff { get; set; }
    public Guid BusinessId { get; set; }
    public Business Business { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public enum CommissionType
{
    Percentage = 1,
    FixedAmount = 2
}