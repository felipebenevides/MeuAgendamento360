using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Reports.Queries;

public class GetStaffPerformanceQueryHandler : IRequestHandler<GetStaffPerformanceQuery, GetStaffPerformanceResponse>
{
    private readonly IApplicationDbContext _context;

    public GetStaffPerformanceQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetStaffPerformanceResponse> Handle(GetStaffPerformanceQuery request, CancellationToken cancellationToken)
    {
        var staffAppointments = await _context.Appointments
            .Include(a => a.Staff)
            .ThenInclude(s => s.User)
            .ThenInclude(u => u.Person)
            .Where(a => a.BusinessId == request.BusinessId &&
                       a.ScheduledAt >= request.StartDate &&
                       a.ScheduledAt <= request.EndDate)
            .GroupBy(a => new { a.StaffId, StaffName = $"{a.Staff.User.Person.FirstName} {a.Staff.User.Person.LastName}" })
            .Select(g => new StaffPerformanceDto(
                g.Key.StaffId,
                g.Key.StaffName,
                g.Count(),
                g.Count(a => a.Status == AppointmentStatus.Completed),
                g.Count(a => a.Status == AppointmentStatus.Cancelled),
                g.Where(a => a.Status == AppointmentStatus.Completed).Sum(a => a.Price),
                g.Count() > 0 ? (decimal)g.Count(a => a.Status == AppointmentStatus.Completed) / g.Count() * 100 : 0,
                0 // Average rating placeholder
            ))
            .ToArrayAsync(cancellationToken);

        return new GetStaffPerformanceResponse(staffAppointments);
    }
}