using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Services;

public class MultiStoreService : IMultiStoreService
{
    private readonly IApplicationDbContext _context;

    public MultiStoreService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Store>> GetOwnerStoresAsync(Guid ownerId)
    {
        return await _context.Stores
            .Where(s => s.OwnerId == ownerId)
            .Include(s => s.Address)
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Store>> GetUserStoresAsync(Guid userId)
    {
        var owner = await _context.Owners
            .FirstOrDefaultAsync(o => o.UserId == userId);

        if (owner == null)
            return Enumerable.Empty<Store>();

        return await GetOwnerStoresAsync(owner.Id);
    }

    public async Task<bool> IsOwnerOfStoreAsync(Guid userId, Guid storeId)
    {
        return await _context.Owners
            .AnyAsync(o => o.UserId == userId && o.Businesses.Any(s => s.Id == storeId));
    }

    public async Task<MultiStoreIndicators> GetConsolidatedIndicatorsAsync(Guid ownerId)
    {
        var stores = await GetOwnerStoresAsync(ownerId);
        var storeIds = stores.Select(s => s.Id).ToList();

        var indicators = new MultiStoreIndicators();

        foreach (var store in stores)
        {
            var storeIndicator = await CalculateStoreIndicators(store.Id);
            indicators.StoreBreakdown.Add(new StoreIndicator
            {
                StoreId = store.Id,
                StoreName = store.Name,
                Revenue = storeIndicator.Revenue,
                Appointments = storeIndicator.Appointments,
                Customers = storeIndicator.Customers,
                Expenses = storeIndicator.Expenses,
                NetProfit = storeIndicator.NetProfit
            });

            // Aggregate totals
            indicators.TotalRevenue += storeIndicator.Revenue;
            indicators.TotalAppointments += storeIndicator.Appointments;
            indicators.TotalCustomers += storeIndicator.Customers;
            indicators.TotalExpenses += storeIndicator.Expenses;
        }

        indicators.NetProfit = indicators.TotalRevenue - indicators.TotalExpenses;

        return indicators;
    }

    public async Task<MultiStoreIndicators> GetStoreSpecificIndicatorsAsync(Guid storeId)
    {
        var store = await _context.Stores.FindAsync(storeId);
        if (store == null)
            return new MultiStoreIndicators();

        var storeIndicator = await CalculateStoreIndicators(storeId);

        return new MultiStoreIndicators
        {
            TotalRevenue = storeIndicator.Revenue,
            TotalAppointments = storeIndicator.Appointments,
            TotalCustomers = storeIndicator.Customers,
            TotalExpenses = storeIndicator.Expenses,
            NetProfit = storeIndicator.NetProfit,
            StoreBreakdown = new List<StoreIndicator> { storeIndicator }
        };
    }

    private async Task<StoreIndicator> CalculateStoreIndicators(Guid storeId)
    {
        var currentMonth = DateTime.UtcNow.Month;
        var currentYear = DateTime.UtcNow.Year;

        // Calculate revenue from appointments
        var revenue = await _context.Appointments
            .Where(a => a.Service.StoreId == storeId && 
                       a.Status == AppointmentStatus.Completed &&
                       a.ScheduledAt.Month == currentMonth &&
                       a.ScheduledAt.Year == currentYear)
            .SumAsync(a => a.Price);

        // Count appointments
        var appointments = await _context.Appointments
            .CountAsync(a => a.Service.StoreId == storeId &&
                           a.ScheduledAt.Month == currentMonth &&
                           a.ScheduledAt.Year == currentYear);

        // Count unique customers
        var customers = await _context.Appointments
            .Where(a => a.Service.StoreId == storeId &&
                       a.ScheduledAt.Month == currentMonth &&
                       a.ScheduledAt.Year == currentYear)
            .Select(a => a.CustomerId)
            .Distinct()
            .CountAsync();

        // Calculate expenses
        var expenses = await _context.FinancialTransactions
            .Where(ft => ft.StoreId == storeId &&
                        ft.Type == TransactionType.Expense &&
                        ft.TransactionDate.Month == currentMonth &&
                        ft.TransactionDate.Year == currentYear)
            .SumAsync(ft => ft.Amount);

        var store = await _context.Stores.FindAsync(storeId);

        return new StoreIndicator
        {
            StoreId = storeId,
            StoreName = store?.Name ?? "",
            Revenue = revenue,
            Appointments = appointments,
            Customers = customers,
            Expenses = expenses,
            NetProfit = revenue - expenses
        };
    }
}