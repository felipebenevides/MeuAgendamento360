using MediatR;

namespace myschedule360.Application.Features.Customers.Commands;

public record RegisterCustomerCommand(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string? CPF,
    DateTime? BirthDate,
    Guid BusinessId,
    string? Notes
) : IRequest<RegisterCustomerResponse>;

public record RegisterCustomerResponse(
    Guid CustomerId,
    Guid PersonId,
    string FirstName,
    string LastName,
    string Email
);