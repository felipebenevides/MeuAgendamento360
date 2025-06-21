using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Services.Commands;

public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, UpdateServiceResponse>
{
    private readonly IApplicationDbContext _context;

    public UpdateServiceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateServiceResponse> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _context.Services
            .FirstOrDefaultAsync(s => s.Id == request.ServiceId, cancellationToken);

        if (service == null)
            throw new InvalidOperationException("Service not found");

        service.Name = request.Name;
        service.Description = request.Description;
        service.Price = request.Price;
        service.DurationMinutes = request.DurationMinutes;
        service.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateServiceResponse(
            service.Id,
            service.Name,
            service.Price,
            service.DurationMinutes,
            service.UpdatedAt.Value
        );
    }
}