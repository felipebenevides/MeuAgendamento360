using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.BusinessOnboarding.Queries;

public class GetBusinessSetupStatusQueryHandler : IRequestHandler<GetBusinessSetupStatusQuery, GetBusinessSetupStatusResponse>
{
    private readonly IApplicationDbContext _context;

    public GetBusinessSetupStatusQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetBusinessSetupStatusResponse> Handle(GetBusinessSetupStatusQuery request, CancellationToken cancellationToken)
    {
        var business = await _context.Businesses
            .Include(b => b.Address)
            .FirstOrDefaultAsync(b => b.Id == request.BusinessId, cancellationToken);

        if (business == null)
            throw new InvalidOperationException("Business not found");

        var missingSteps = new List<string>();
        var completedSteps = 0;
        var totalSteps = 4;

        // Check required fields
        if (string.IsNullOrEmpty(business.Description)) missingSteps.Add("Business description");
        else completedSteps++;

        if (business.Address == null) missingSteps.Add("Business address");
        else completedSteps++;

        if (business.CNPJ == null) missingSteps.Add("CNPJ registration");
        else completedSteps++;

        // Check if has services
        var hasServices = await _context.Services.AnyAsync(s => s.BusinessId == request.BusinessId, cancellationToken);
        if (!hasServices) missingSteps.Add("At least one service");
        else completedSteps++;

        var completionPercentage = (int)((double)completedSteps / totalSteps * 100);

        return new GetBusinessSetupStatusResponse(
            business.Id,
            business.Name,
            business.Slug,
            business.IsOnboardingComplete,
            completionPercentage,
            missingSteps.ToArray()
        );
    }
}