using MediatR;

namespace myschedule360.Application.Features.Authentication.Commands;

public record RegisterBusinessOwnerCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string CPF,
    string Phone,
    string BusinessName,
    string BusinessType,
    string? CNPJ,
    string Street,
    string Number,
    string? Complement,
    string Neighborhood,
    string City,
    string State,
    string CEP,
    decimal? Latitude,
    decimal? Longitude
) : IRequest<RegisterBusinessOwnerResponse>;

public record RegisterBusinessOwnerResponse(
    Guid UserId,
    Guid PersonId,
    Guid BusinessId,
    string Token,
    string RefreshToken
);