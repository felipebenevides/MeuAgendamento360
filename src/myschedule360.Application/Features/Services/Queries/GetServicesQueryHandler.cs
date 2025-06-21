using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Services.Queries;

public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, GetServicesResponse>
{
    private readonly IApplicationDbContext _context;

    public GetServicesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetServicesResponse> Handle(GetServicesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Services
            .Where(s => s.BusinessId == request.BusinessId);

        if (request.IsActive.HasValue)
            query = query.Where(s => s.IsActive == request.IsActive.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var services = await query
            .OrderBy(s => s.Name)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(s => new ServiceDto(
                s.Id,
                s.Name,
                s.Description,
                s.Price,
                s.DurationMinutes,
                s.IsActive,
                s.CreatedAt,
                s.UpdatedAt
            ))
            .ToArrayAsync(cancellationToken);

        return new GetServicesResponse(
            services,
            totalCount,
            request.Page,
            request.PageSize
        );
    }
}