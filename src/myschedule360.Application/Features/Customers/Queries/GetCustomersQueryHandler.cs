using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Customers.Queries;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, GetCustomersResponse>
{
    private readonly IApplicationDbContext _context;

    public GetCustomersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetCustomersResponse> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Customers
            .Include(c => c.Person)
            .Where(c => c.BusinessId == request.BusinessId);

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.ToLower();
            query = query.Where(c => 
                c.Person.FirstName.ToLower().Contains(searchTerm) ||
                c.Person.LastName.ToLower().Contains(searchTerm) ||
                c.Person.Email.ToLower().Contains(searchTerm));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var customers = await query
            .OrderBy(c => c.Person.FirstName)
            .ThenBy(c => c.Person.LastName)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(c => new CustomerDto(
                c.Id,
                c.PersonId,
                c.Person.FirstName,
                c.Person.LastName,
                c.Person.Email,
                c.Person.Phone != null ? c.Person.Phone.Value : "",
                c.Person.BirthDate,
                c.Notes,
                c.Appointments.Count,
                c.CreatedAt
            ))
            .ToArrayAsync(cancellationToken);

        return new GetCustomersResponse(
            customers,
            totalCount,
            request.Page,
            request.PageSize
        );
    }
}