using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Services.Commands;

public class ToggleServiceStatusCommandHandler : IRequestHandler<ToggleServiceStatusCommand, ToggleServiceStatusResponse>
{
    private readonly IApplicationDbContext _context;

    public ToggleServiceStatusCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ToggleServiceStatusResponse> Handle(ToggleServiceStatusCommand request, CancellationToken cancellationToken)
    {
        var service = await _context.Services
            .FirstOrDefaultAsync(s => s.Id == request.ServiceId, cancellationToken);

        if (service == null)
            throw new InvalidOperationException("Service not found");

        service.IsActive = !service.IsActive;
        service.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new ToggleServiceStatusResponse(
            service.Id,
            service.IsActive,
            service.UpdatedAt.Value
        );
    }
}