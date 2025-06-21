using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Commission.Queries;

public class GetCommissionReportQueryHandler : IRequestHandler<GetCommissionReportQuery, GetCommissionReportResponse>
{
    private readonly IApplicationDbContext _context;

    public GetCommissionReportQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetCommissionReportResponse> Handle(GetCommissionReportQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Appointments
            .Include(a => a.Staff)
            .ThenInclude(s => s.User)
            .ThenInclude(u => u.Person)
            .Where(a => a.BusinessId == request.BusinessId &&
                       a.ScheduledAt >= request.StartDate &&
                       a.ScheduledAt <= request.EndDate &&
                       a.Status == AppointmentStatus.Completed);

        if (request.StaffId.HasValue)
            query = query.Where(a => a.StaffId == request.StaffId.Value);

        var appointments = await query.ToListAsync(cancellationToken);

        var staffCommissions = appointments
            .GroupBy(a => new { a.StaffId, StaffName = $"{a.Staff.User.Person.FirstName} {a.Staff.User.Person.LastName}" })
            .Select(g => new CommissionReportDto(
                g.Key.StaffId,
                g.Key.StaffName,
                g.Count(),
                g.Sum(a => a.Price),
                g.Sum(a => a.Price * ((a.Staff.CommissionRate ?? 0) / 100))
            ))
            .ToArray();

        return new GetCommissionReportResponse(
            staffCommissions,
            staffCommissions.Sum(s => s.TotalCommission)
        );
    }
}