using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;
using myschedule360.Domain.ValueObjects;

namespace myschedule360.Application.Features.Customers.Commands;

public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, RegisterCustomerResponse>
{
    private readonly IApplicationDbContext _context;

    public RegisterCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RegisterCustomerResponse> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        // Check if customer already exists for this business
        var existingCustomer = await _context.Customers
            .Include(c => c.Person)
            .FirstOrDefaultAsync(c => c.Person.Email == request.Email && c.BusinessId == request.BusinessId, cancellationToken);

        if (existingCustomer != null)
            throw new InvalidOperationException("Customer already exists for this business");

        // Create person
        var person = new Person
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = new Phone(request.Phone),
            CPF = !string.IsNullOrEmpty(request.CPF) ? new CPF(request.CPF) : null,
            BirthDate = request.BirthDate,
            Type = PersonType.Customer
        };

        // Create customer
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            PersonId = person.Id,
            BusinessId = request.BusinessId,
            Notes = request.Notes
        };

        _context.Persons.Add(person);
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return new RegisterCustomerResponse(
            customer.Id,
            person.Id,
            person.FirstName,
            person.LastName,
            person.Email
        );
    }
}