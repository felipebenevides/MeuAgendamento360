using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.BusinessOnboarding.Commands;

public class UpdateBusinessInfoCommandHandler : IRequestHandler<UpdateBusinessInfoCommand, UpdateBusinessInfoResponse>
{
    private readonly IApplicationDbContext _context;

    public UpdateBusinessInfoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateBusinessInfoResponse> Handle(UpdateBusinessInfoCommand request, CancellationToken cancellationToken)
    {
        var business = await _context.Businesses
            .FirstOrDefaultAsync(b => b.Id == request.BusinessId, cancellationToken);

        if (business == null)
            throw new InvalidOperationException("Business not found");

        business.Name = request.Name;
        business.Description = request.Description;
        business.Website = request.Website;
        business.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateBusinessInfoResponse(
            business.Id,
            business.Name,
            business.Slug,
            business.UpdatedAt.Value
        );
    }
}