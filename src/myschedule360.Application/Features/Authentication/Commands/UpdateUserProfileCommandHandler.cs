using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;
using myschedule360.Domain.ValueObjects;

namespace myschedule360.Application.Features.Authentication.Commands;

public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, UpdateUserProfileResponse>
{
    private readonly IApplicationDbContext _context;

    public UpdateUserProfileCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateUserProfileResponse> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Person)
            .FirstOrDefaultAsync(u => u.Id == request.UserId && u.IsActive, cancellationToken);

        if (user == null)
            throw new InvalidOperationException("User not found");

        user.Person.FirstName = request.FirstName;
        user.Person.LastName = request.LastName;
        user.Person.Phone = new Phone(request.Phone);
        user.Person.BirthDate = request.BirthDate;
        user.Person.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateUserProfileResponse(
            user.Id,
            user.Person.FirstName,
            user.Person.LastName,
            user.Person.Phone.Value,
            user.Person.BirthDate,
            user.Person.UpdatedAt.Value
        );
    }
}