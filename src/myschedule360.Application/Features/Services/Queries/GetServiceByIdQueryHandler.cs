using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Services.Queries;

public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, GetServiceByIdResponse>
{
    private readonly IApplicationDbContext _context;

    public GetServiceByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetServiceByIdResponse> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
    {
        var service = await _context.Services
            .FirstOrDefaultAsync(s => s.Id == request.ServiceId, cancellationToken);

        if (service == null)
            throw new InvalidOperationException("Service not found");

        return new GetServiceByIdResponse(
            service.Id,
            service.Name,
            service.Description,
            service.Price,
            service.DurationMinutes,
            service.IsActive,
            service.BusinessId,
            service.CreatedAt,
            service.UpdatedAt
        );
    }
}