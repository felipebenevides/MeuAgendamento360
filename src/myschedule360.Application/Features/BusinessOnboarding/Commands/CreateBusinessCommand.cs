using MediatR;

namespace myschedule360.Application.Features.BusinessOnboarding.Commands;

public record CreateBusinessCommand(
    string Name,
    string Type,
    string? Description,
    string? Address,
    string? Phone,
    string? Email,
    string? Website
) : IRequest<CreateBusinessResponse>;

public record CreateBusinessResponse(
    Guid BusinessId,
    string Name,
    string Slug,
    bool IsOnboardingComplete
);