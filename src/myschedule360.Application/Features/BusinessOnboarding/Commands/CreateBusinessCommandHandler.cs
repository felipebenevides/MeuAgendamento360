using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.BusinessOnboarding.Commands;

public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, CreateBusinessResponse>
{
    private readonly IApplicationDbContext _context;

    public CreateBusinessCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CreateBusinessResponse> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
    {
        var slug = GenerateSlug(request.Name);
        
        // Check if slug already exists
        if (await _context.Businesses.AnyAsync(b => b.Slug == slug, cancellationToken))
            throw new InvalidOperationException("Business name already exists");

        var business = new Business
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Slug = slug,
            Type = Enum.Parse<BusinessType>(request.Type),
            Description = request.Description,
            Website = request.Website,
            IsOnboardingComplete = false
        };

        _context.Businesses.Add(business);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateBusinessResponse(
            business.Id,
            business.Name,
            business.Slug,
            business.IsOnboardingComplete
        );
    }

    private static string GenerateSlug(string businessName)
    {
        return businessName.ToLowerInvariant()
            .Replace(" ", "-")
            .Replace(".", "")
            .Replace(",", "")
            .Replace("&", "and");
    }
}