using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Customers.Queries;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdResponse>
{
    private readonly IApplicationDbContext _context;

    public GetCustomerByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .Include(c => c.Person)
            .Include(c => c.Appointments)
            .FirstOrDefaultAsync(c => c.Id == request.CustomerId, cancellationToken);

        if (customer == null)
            throw new InvalidOperationException("Customer not found");

        return new GetCustomerByIdResponse(
            customer.Id,
            customer.PersonId,
            customer.Person.FirstName,
            customer.Person.LastName,
            customer.Person.Email,
            customer.Person.Phone?.Value ?? "",
            customer.Person.CPF?.Value,
            customer.Person.BirthDate,
            customer.Notes,
            customer.Appointments.Count,
            customer.CreatedAt,
            customer.UpdatedAt
        );
    }
}