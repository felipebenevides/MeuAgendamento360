using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.BusinessOnboarding.Commands;

public class CompleteOnboardingCommandHandler : IRequestHandler<CompleteOnboardingCommand, CompleteOnboardingResponse>
{
    private readonly IApplicationDbContext _context;

    public CompleteOnboardingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CompleteOnboardingResponse> Handle(CompleteOnboardingCommand request, CancellationToken cancellationToken)
    {
        var business = await _context.Businesses
            .FirstOrDefaultAsync(b => b.Id == request.BusinessId, cancellationToken);

        if (business == null)
            throw new InvalidOperationException("Business not found");

        business.IsOnboardingComplete = true;
        business.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new CompleteOnboardingResponse(
            business.Id,
            business.IsOnboardingComplete,
            business.UpdatedAt.Value
        );
    }
}