using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;
using myschedule360.Domain.ValueObjects;

namespace myschedule360.Application.Features.Customers.Commands;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerResponse>
{
    private readonly IApplicationDbContext _context;

    public UpdateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateCustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .Include(c => c.Person)
            .FirstOrDefaultAsync(c => c.Id == request.CustomerId, cancellationToken);

        if (customer == null)
            throw new InvalidOperationException("Customer not found");

        customer.Person.FirstName = request.FirstName;
        customer.Person.LastName = request.LastName;
        customer.Person.Phone = new Phone(request.Phone);
        customer.Person.BirthDate = request.BirthDate;
        customer.Person.UpdatedAt = DateTime.UtcNow;
        customer.Notes = request.Notes;
        customer.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateCustomerResponse(
            customer.Id,
            customer.Person.FirstName,
            customer.Person.LastName,
            customer.Person.Phone.Value,
            customer.Person.BirthDate,
            customer.Person.UpdatedAt.Value
        );
    }
}