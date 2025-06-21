using MediatR;

namespace myschedule360.Application.Features.BusinessOnboarding.Commands;

public record UpdateBusinessInfoCommand(
    Guid BusinessId,
    string Name,
    string? Description,
    string? Website
) : IRequest<UpdateBusinessInfoResponse>;

public record UpdateBusinessInfoResponse(
    Guid BusinessId,
    string Name,
    string Slug,
    DateTime UpdatedAt
);