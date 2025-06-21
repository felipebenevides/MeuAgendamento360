using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.BusinessOnboarding.Queries;

public class GetBusinessInfoQueryHandler : IRequestHandler<GetBusinessInfoQuery, GetBusinessInfoResponse>
{
    private readonly IApplicationDbContext _context;

    public GetBusinessInfoQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetBusinessInfoResponse> Handle(GetBusinessInfoQuery request, CancellationToken cancellationToken)
    {
        var business = await _context.Businesses
            .FirstOrDefaultAsync(b => b.Id == request.BusinessId, cancellationToken);

        if (business == null)
            throw new InvalidOperationException("Business not found");

        return new GetBusinessInfoResponse(
            business.Id,
            business.Name,
            business.Slug,
            business.Type.ToString(),
            business.Description,
            business.Website,
            business.IsOnboardingComplete,
            business.CreatedAt,
            business.UpdatedAt
        );
    }
}