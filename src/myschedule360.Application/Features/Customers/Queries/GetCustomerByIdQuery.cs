using MediatR;

namespace myschedule360.Application.Features.Customers.Queries;

public record GetCustomerByIdQuery(Guid CustomerId) : IRequest<GetCustomerByIdResponse>;

public record GetCustomerByIdResponse(
    Guid CustomerId,
    Guid PersonId,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string? CPF,
    DateTime? BirthDate,
    string? Notes,
    int TotalAppointments,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);