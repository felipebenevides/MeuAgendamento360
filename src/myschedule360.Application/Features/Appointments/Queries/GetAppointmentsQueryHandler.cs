using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Appointments.Queries;

public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, GetAppointmentsResponse>
{
    private readonly IApplicationDbContext _context;

    public GetAppointmentsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetAppointmentsResponse> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Appointments
            .Include(a => a.Service)
            .Include(a => a.Customer)
            .ThenInclude(c => c.Person)
            .Include(a => a.Staff)
            .ThenInclude(s => s.User)
            .ThenInclude(u => u.Person)
            .Where(a => a.BusinessId == request.BusinessId);

        if (request.StartDate.HasValue)
            query = query.Where(a => a.ScheduledAt >= request.StartDate.Value);

        if (request.EndDate.HasValue)
            query = query.Where(a => a.ScheduledAt <= request.EndDate.Value);

        if (request.StaffId.HasValue)
            query = query.Where(a => a.StaffId == request.StaffId.Value);

        if (!string.IsNullOrEmpty(request.Status) && Enum.TryParse<AppointmentStatus>(request.Status, out var status))
            query = query.Where(a => a.Status == status);

        var totalCount = await query.CountAsync(cancellationToken);

        var appointments = await query
            .OrderBy(a => a.ScheduledAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(a => new AppointmentDto(
                a.Id,
                a.ScheduledAt,
                a.DurationMinutes,
                a.Service.Name,
                $"{a.Customer.Person.FirstName} {a.Customer.Person.LastName}",
                $"{a.Staff.User.Person.FirstName} {a.Staff.User.Person.LastName}",
                a.Price,
                a.Status.ToString(),
                a.Notes
            ))
            .ToArrayAsync(cancellationToken);

        return new GetAppointmentsResponse(
            appointments,
            totalCount,
            request.Page,
            request.PageSize
        );
    }
}