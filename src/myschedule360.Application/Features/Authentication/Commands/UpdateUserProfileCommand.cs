using MediatR;

namespace myschedule360.Application.Features.Authentication.Commands;

public record UpdateUserProfileCommand(
    Guid UserId,
    string FirstName,
    string LastName,
    string Phone,
    DateTime? BirthDate
) : IRequest<UpdateUserProfileResponse>;

public record UpdateUserProfileResponse(
    Guid UserId,
    string FirstName,
    string LastName,
    string Phone,
    DateTime? BirthDate,
    DateTime UpdatedAt
);