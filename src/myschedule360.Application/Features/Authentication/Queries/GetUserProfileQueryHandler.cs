using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Authentication.Queries;

public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, GetUserProfileResponse>
{
    private readonly IApplicationDbContext _context;

    public GetUserProfileQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetUserProfileResponse> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Person)
            .Include(u => u.Business)
            .FirstOrDefaultAsync(u => u.Id == request.UserId && u.IsActive, cancellationToken);

        if (user == null)
            throw new InvalidOperationException("User not found");

        return new GetUserProfileResponse(
            user.Id,
            user.Email,
            user.Person.FirstName,
            user.Person.LastName,
            user.Role.ToString(),
            user.BusinessId,
            user.Business?.Name,
            user.CreatedAt
        );
    }
}