namespace myschedule360.Domain.Entities;

public class FinancialTransaction
{
    public Guid Id { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? Category { get; set; }
    public DateTime TransactionDate { get; set; }
    public Guid? AppointmentId { get; set; }
    public Appointment? Appointment { get; set; }
    public Guid StoreId { get; set; }
    public Guid BusinessId { get; set; }
    public Store Store { get; set; } = null!;
    public Business Business { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public enum TransactionType
{
    Income = 1,
    Expense = 2
}