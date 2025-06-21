using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;
using myschedule360.Domain.ValueObjects;

namespace myschedule360.Application.Features.Authentication.Commands;

public class AddServiceProviderCommandHandler : IRequestHandler<AddServiceProviderCommand, AddServiceProviderResponse>
{
    private readonly IApplicationDbContext _context;

    public AddServiceProviderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AddServiceProviderResponse> Handle(AddServiceProviderCommand request, CancellationToken cancellationToken)
    {
        // Check if email already exists
        if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            throw new InvalidOperationException("Email already exists");

        // Verify business exists
        if (!await _context.Businesses.AnyAsync(b => b.Id == request.BusinessId, cancellationToken))
            throw new InvalidOperationException("Business not found");

        // Create person
        var person = new Person
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = new Phone(request.Phone),
            CPF = new CPF(request.CPF),
            Type = PersonType.ServiceProvider
        };

        // Create user
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = UserRole.ServiceProvider,
            BusinessId = request.BusinessId,
            PersonId = person.Id,
            IsActive = true
        };

        _context.Persons.Add(person);
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddServiceProviderResponse(
            user.Id,
            person.Id,
            user.Email,
            person.FirstName,
            person.LastName
        );
    }
}