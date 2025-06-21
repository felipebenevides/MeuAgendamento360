using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Reports.Queries;

public class GetBusinessDashboardQueryHandler : IRequestHandler<GetBusinessDashboardQuery, GetBusinessDashboardResponse>
{
    private readonly IApplicationDbContext _context;

    public GetBusinessDashboardQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetBusinessDashboardResponse> Handle(GetBusinessDashboardQuery request, CancellationToken cancellationToken)
    {
        var appointments = await _context.Appointments
            .Include(a => a.Service)
            .Where(a => a.BusinessId == request.BusinessId &&
                       a.ScheduledAt >= request.StartDate &&
                       a.ScheduledAt <= request.EndDate)
            .ToListAsync(cancellationToken);

        var totalCustomers = await _context.Customers
            .CountAsync(c => c.BusinessId == request.BusinessId, cancellationToken);

        var activeStaff = await _context.Staff
            .CountAsync(s => s.BusinessId == request.BusinessId && s.IsActive, cancellationToken);

        var appointmentsByStatus = new AppointmentStatusSummaryDto(
            appointments.Count(a => a.Status == AppointmentStatus.Scheduled),
            appointments.Count(a => a.Status == AppointmentStatus.Confirmed),
            appointments.Count(a => a.Status == AppointmentStatus.Completed),
            appointments.Count(a => a.Status == AppointmentStatus.Cancelled),
            appointments.Count(a => a.Status == AppointmentStatus.NoShow)
        );

        var topServices = appointments
            .Where(a => a.Status == AppointmentStatus.Completed)
            .GroupBy(a => a.Service.Name)
            .Select(g => new TopServiceDto(
                g.Key,
                g.Count(),
                g.Sum(a => a.Price)
            ))
            .OrderByDescending(s => s.Revenue)
            .Take(5)
            .ToArray();

        var revenueByDay = appointments
            .Where(a => a.Status == AppointmentStatus.Completed)
            .GroupBy(a => a.ScheduledAt.Date)
            .Select(g => new RevenueByDayDto(
                g.Key,
                g.Sum(a => a.Price),
                g.Count()
            ))
            .OrderBy(r => r.Date)
            .ToArray();

        return new GetBusinessDashboardResponse(
            appointments.Count,
            appointments.Where(a => a.Status == AppointmentStatus.Completed).Sum(a => a.Price),
            totalCustomers,
            activeStaff,
            appointmentsByStatus,
            topServices,
            revenueByDay
        );
    }
}