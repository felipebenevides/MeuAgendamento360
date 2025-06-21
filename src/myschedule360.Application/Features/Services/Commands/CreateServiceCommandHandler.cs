using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Services.Commands;

public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, CreateServiceResponse>
{
    private readonly IApplicationDbContext _context;

    public CreateServiceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CreateServiceResponse> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        // Verify business exists
        if (!await _context.Businesses.AnyAsync(b => b.Id == request.BusinessId, cancellationToken))
            throw new InvalidOperationException("Business not found");

        var service = new Service
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            DurationMinutes = request.DurationMinutes,
            BusinessId = request.BusinessId,
            IsActive = request.IsActive
        };

        _context.Services.Add(service);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateServiceResponse(
            service.Id,
            service.Name,
            service.Price,
            service.DurationMinutes,
            service.IsActive
        );
    }
}