using MediatR;

namespace myschedule360.Application.Features.BusinessOnboarding.Queries;

public record GetBusinessInfoQuery(Guid BusinessId) : IRequest<GetBusinessInfoResponse>;

public record GetBusinessInfoResponse(
    Guid Id,
    string Name,
    string Slug,
    string Type,
    string? Description,
    string? Website,
    bool IsOnboardingComplete,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);