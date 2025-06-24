using myschedule360.Domain.Entities;

namespace myschedule360.Application.Services;

public interface IMultiStoreService
{
    Task<IEnumerable<Store>> GetOwnerStoresAsync(Guid ownerId);
    Task<IEnumerable<Store>> GetUserStoresAsync(Guid userId);
    Task<bool> IsOwnerOfStoreAsync(Guid userId, Guid storeId);
    Task<MultiStoreIndicators> GetConsolidatedIndicatorsAsync(Guid ownerId);
    Task<MultiStoreIndicators> GetStoreSpecificIndicatorsAsync(Guid storeId);
}

public class MultiStoreIndicators
{
    public decimal TotalRevenue { get; set; }
    public int TotalAppointments { get; set; }
    public int TotalCustomers { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal NetProfit { get; set; }
    public List<StoreIndicator> StoreBreakdown { get; set; } = new();
}

public class StoreIndicator
{
    public Guid StoreId { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public decimal Revenue { get; set; }
    public int Appointments { get; set; }
    public int Customers { get; set; }
    public decimal Expenses { get; set; }
    public decimal NetProfit { get; set; }
}