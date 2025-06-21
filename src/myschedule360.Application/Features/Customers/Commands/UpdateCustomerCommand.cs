using MediatR;

namespace myschedule360.Application.Features.Customers.Commands;

public record UpdateCustomerCommand(
    Guid CustomerId,
    string FirstName,
    string LastName,
    string Phone,
    DateTime? BirthDate,
    string? Notes
) : IRequest<UpdateCustomerResponse>;

public record UpdateCustomerResponse(
    Guid CustomerId,
    string FirstName,
    string LastName,
    string Phone,
    DateTime? BirthDate,
    DateTime UpdatedAt
);