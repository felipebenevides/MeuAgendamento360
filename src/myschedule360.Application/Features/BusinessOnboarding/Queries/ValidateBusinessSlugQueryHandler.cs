using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.BusinessOnboarding.Queries;

public class ValidateBusinessSlugQueryHandler : IRequestHandler<ValidateBusinessSlugQuery, ValidateBusinessSlugResponse>
{
    private readonly IApplicationDbContext _context;

    public ValidateBusinessSlugQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ValidateBusinessSlugResponse> Handle(ValidateBusinessSlugQuery request, CancellationToken cancellationToken)
    {
        var slug = request.Slug.ToLowerInvariant();
        var isAvailable = !await _context.Businesses.AnyAsync(b => b.Slug == slug, cancellationToken);

        string? suggestedSlug = null;
        if (!isAvailable)
        {
            // Generate suggested slug with number suffix
            var counter = 1;
            do
            {
                suggestedSlug = $"{slug}-{counter}";
                counter++;
            } while (await _context.Businesses.AnyAsync(b => b.Slug == suggestedSlug, cancellationToken));
        }

        return new ValidateBusinessSlugResponse(
            slug,
            isAvailable,
            suggestedSlug
        );
    }
}