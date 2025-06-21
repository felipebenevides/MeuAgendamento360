using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Customers.Queries;

public class GetCustomerAppointmentHistoryQueryHandler : IRequestHandler<GetCustomerAppointmentHistoryQuery, GetCustomerAppointmentHistoryResponse>
{
    private readonly IApplicationDbContext _context;

    public GetCustomerAppointmentHistoryQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetCustomerAppointmentHistoryResponse> Handle(GetCustomerAppointmentHistoryQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Appointments
            .Include(a => a.Service)
            .Include(a => a.Staff)
            .ThenInclude(s => s.User)
            .ThenInclude(u => u.Person)
            .Where(a => a.CustomerId == request.CustomerId);

        var totalCount = await query.CountAsync(cancellationToken);

        var appointments = await query
            .OrderByDescending(a => a.ScheduledAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(a => new CustomerAppointmentDto(
                a.Id,
                a.ScheduledAt,
                a.Service.Name,
                $"{a.Staff.User.Person.FirstName} {a.Staff.User.Person.LastName}",
                a.Price,
                a.Status.ToString(),
                a.Notes
            ))
            .ToArrayAsync(cancellationToken);

        return new GetCustomerAppointmentHistoryResponse(
            request.CustomerId,
            appointments,
            totalCount,
            request.Page,
            request.PageSize
        );
    }
}