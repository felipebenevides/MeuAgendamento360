using MediatR;

namespace myschedule360.Application.Features.Authentication.Commands;

public record AddServiceProviderCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string CPF,
    string Phone,
    Guid BusinessId
) : IRequest<AddServiceProviderResponse>;

public record AddServiceProviderResponse(
    Guid UserId,
    Guid PersonId,
    string Email,
    string FirstName,
    string LastName
);