using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Staff.Queries;

public class GetStaffMembersQueryHandler : IRequestHandler<GetStaffMembersQuery, GetStaffMembersResponse>
{
    private readonly IApplicationDbContext _context;

    public GetStaffMembersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetStaffMembersResponse> Handle(GetStaffMembersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Staff
            .Include(s => s.User)
            .ThenInclude(u => u.Person)
            .Where(s => s.BusinessId == request.BusinessId);

        if (request.IsActive.HasValue)
            query = query.Where(s => s.IsActive == request.IsActive.Value);

        var staffMembers = await query
            .Select(s => new StaffMemberDto(
                s.Id,
                s.UserId,
                s.User.Person.FirstName,
                s.User.Person.LastName,
                s.User.Person.Email,
                s.Role,
                s.CommissionRate,
                s.IsActive,
                s.CreatedAt
            ))
            .ToArrayAsync(cancellationToken);

        return new GetStaffMembersResponse(staffMembers);
    }
}