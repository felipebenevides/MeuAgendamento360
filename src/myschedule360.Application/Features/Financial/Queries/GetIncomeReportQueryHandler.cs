using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Financial.Queries;

public class GetIncomeReportQueryHandler : IRequestHandler<GetIncomeReportQuery, GetIncomeReportResponse>
{
    private readonly IApplicationDbContext _context;

    public GetIncomeReportQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetIncomeReportResponse> Handle(GetIncomeReportQuery request, CancellationToken cancellationToken)
    {
        // Get completed appointments for service breakdown
        var appointments = await _context.Appointments
            .Include(a => a.Service)
            .Where(a => a.BusinessId == request.BusinessId &&
                       a.ScheduledAt >= request.StartDate &&
                       a.ScheduledAt <= request.EndDate &&
                       a.Status == AppointmentStatus.Completed)
            .ToListAsync(cancellationToken);

        var incomeByService = appointments
            .GroupBy(a => a.Service.Name)
            .Select(g => new IncomeByServiceDto(
                g.Key,
                g.Count(),
                g.Sum(a => a.Price)
            ))
            .OrderByDescending(s => s.TotalIncome)
            .ToArray();

        var incomeByDay = appointments
            .GroupBy(a => a.ScheduledAt.Date)
            .Select(g => new IncomeByDayDto(
                g.Key,
                g.Sum(a => a.Price)
            ))
            .OrderBy(d => d.Date)
            .ToArray();

        return new GetIncomeReportResponse(
            appointments.Sum(a => a.Price),
            incomeByService,
            incomeByDay
        );
    }
}