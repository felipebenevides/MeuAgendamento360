using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Customers.Commands;

public class AddCustomerNotesCommandHandler : IRequestHandler<AddCustomerNotesCommand, AddCustomerNotesResponse>
{
    private readonly IApplicationDbContext _context;

    public AddCustomerNotesCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AddCustomerNotesResponse> Handle(AddCustomerNotesCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.Id == request.CustomerId, cancellationToken);

        if (customer == null)
            throw new InvalidOperationException("Customer not found");

        customer.Notes = string.IsNullOrEmpty(customer.Notes) 
            ? request.Notes 
            : $"{customer.Notes}\n{DateTime.UtcNow:yyyy-MM-dd HH:mm}: {request.Notes}";
        
        customer.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new AddCustomerNotesResponse(
            customer.Id,
            customer.Notes,
            customer.UpdatedAt.Value
        );
    }
}