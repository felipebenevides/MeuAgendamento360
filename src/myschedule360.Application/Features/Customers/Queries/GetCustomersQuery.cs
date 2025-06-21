using MediatR;

namespace myschedule360.Application.Features.Customers.Queries;

public record GetCustomersQuery(
    Guid BusinessId,
    string? SearchTerm = null,
    int Page = 1,
    int PageSize = 10
) : IRequest<GetCustomersResponse>;

public record GetCustomersResponse(
    CustomerDto[] Customers,
    int TotalCount,
    int Page,
    int PageSize
);

public record CustomerDto(
    Guid CustomerId,
    Guid PersonId,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    DateTime? BirthDate,
    string? Notes,
    int AppointmentCount,
    DateTime CreatedAt
);