using MediatR;

namespace myschedule360.Application.Features.BusinessOnboarding.Queries;

public record ValidateBusinessSlugQuery(string Slug) : IRequest<ValidateBusinessSlugResponse>;

public record ValidateBusinessSlugResponse(
    string Slug,
    bool IsAvailable,
    string? SuggestedSlug
);