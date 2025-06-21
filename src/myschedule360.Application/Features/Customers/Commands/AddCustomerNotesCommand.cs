using MediatR;

namespace myschedule360.Application.Features.Customers.Commands;

public record AddCustomerNotesCommand(
    Guid CustomerId,
    string Notes
) : IRequest<AddCustomerNotesResponse>;

public record AddCustomerNotesResponse(
    Guid CustomerId,
    string Notes,
    DateTime UpdatedAt
);