using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;
using myschedule360.Domain.ValueObjects;

namespace myschedule360.Application.Features.Authentication.Commands;

public class RegisterBusinessOwnerCommandHandler : IRequestHandler<RegisterBusinessOwnerCommand, RegisterBusinessOwnerResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtTokenService _jwtTokenService;

    public RegisterBusinessOwnerCommandHandler(IApplicationDbContext context, IJwtTokenService jwtTokenService)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<RegisterBusinessOwnerResponse> Handle(RegisterBusinessOwnerCommand request, CancellationToken cancellationToken)
    {
        // Check if email already exists
        if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            throw new InvalidOperationException("Email already exists");

        // Create address
        var address = new Address
        {
            Id = Guid.NewGuid(),
            Street = request.Street,
            Number = request.Number,
            Complement = request.Complement,
            Neighborhood = request.Neighborhood,
            City = request.City,
            State = request.State,
            CEP = new CEP(request.CEP),
            Latitude = request.Latitude,
            Longitude = request.Longitude
        };

        // Create business
        var business = new Business
        {
            Id = Guid.NewGuid(),
            Name = request.BusinessName,
            Slug = GenerateSlug(request.BusinessName),
            Type = Enum.Parse<BusinessType>(request.BusinessType),
            CNPJ = !string.IsNullOrEmpty(request.CNPJ) ? new CNPJ(request.CNPJ) : null,
            AddressId = address.Id,
            IsOnboardingComplete = false
        };

        // Create person
        var person = new Person
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = new Phone(request.Phone),
            CPF = new CPF(request.CPF),
            Type = PersonType.BusinessOwner
        };

        // Create user
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = UserRole.BusinessOwner,
            BusinessId = business.Id,
            PersonId = person.Id,
            IsActive = true
        };

        _context.Addresses.Add(address);
        _context.Businesses.Add(business);
        _context.Persons.Add(person);
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        var token = _jwtTokenService.GenerateToken(user);
        var refreshToken = _jwtTokenService.GenerateRefreshToken();

        return new RegisterBusinessOwnerResponse(
            user.Id,
            person.Id,
            business.Id,
            token,
            refreshToken
        );
    }

    private static string GenerateSlug(string businessName)
    {
        return businessName.ToLowerInvariant()
            .Replace(" ", "-")
            .Replace(".", "")
            .Replace(",", "")
            .Replace("&", "and");
    }
}