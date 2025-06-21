using MediatR;

namespace myschedule360.Application.Features.Authentication.Queries;

public record GetUserProfileQuery(Guid UserId) : IRequest<GetUserProfileResponse>;

public record GetUserProfileResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string Role,
    Guid? BusinessId,
    string? BusinessName,
    DateTime CreatedAt
);