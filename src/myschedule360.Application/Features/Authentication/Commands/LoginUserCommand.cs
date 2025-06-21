using MediatR;

namespace myschedule360.Application.Features.Authentication.Commands;

public record LoginUserCommand(
    string Email,
    string Password
) : IRequest<LoginUserResponse>;

public record LoginUserResponse(
    Guid UserId,
    string Email,
    string FirstName,
    string LastName,
    string Role,
    Guid? BusinessId,
    string Token,
    string RefreshToken
);