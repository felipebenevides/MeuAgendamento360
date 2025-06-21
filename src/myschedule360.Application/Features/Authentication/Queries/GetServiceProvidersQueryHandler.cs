using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Authentication.Queries;

public class GetServiceProvidersQueryHandler : IRequestHandler<GetServiceProvidersQuery, GetServiceProvidersResponse>
{
    private readonly IApplicationDbContext _context;

    public GetServiceProvidersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetServiceProvidersResponse> Handle(GetServiceProvidersQuery request, CancellationToken cancellationToken)
    {
        var serviceProviders = await _context.Users
            .Include(u => u.Person)
            .Where(u => u.BusinessId == request.BusinessId && u.Role == UserRole.ServiceProvider)
            .Select(u => new ServiceProviderDto(
                u.Id,
                u.PersonId,
                u.Person.FirstName,
                u.Person.LastName,
                u.Person.Email,
                u.Person.Phone != null ? u.Person.Phone.Value : "",
                u.IsActive,
                u.CreatedAt
            ))
            .ToArrayAsync(cancellationToken);

        return new GetServiceProvidersResponse(serviceProviders);
    }
}