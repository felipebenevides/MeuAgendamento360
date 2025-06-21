using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Staff.Commands;

public class AddStaffMemberCommandHandler : IRequestHandler<AddStaffMemberCommand, AddStaffMemberResponse>
{
    private readonly IApplicationDbContext _context;

    public AddStaffMemberCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AddStaffMemberResponse> Handle(AddStaffMemberCommand request, CancellationToken cancellationToken)
    {
        // Verify user exists and belongs to business
        var user = await _context.Users
            .Include(u => u.Person)
            .FirstOrDefaultAsync(u => u.Id == request.UserId && u.BusinessId == request.BusinessId, cancellationToken);

        if (user == null)
            throw new InvalidOperationException("User not found or doesn't belong to business");

        // Check if staff member already exists
        if (await _context.Staff.AnyAsync(s => s.UserId == request.UserId, cancellationToken))
            throw new InvalidOperationException("User is already a staff member");

        var staff = new Domain.Entities.Staff
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            BusinessId = request.BusinessId,
            Role = request.Role,
            CommissionRate = request.CommissionRate,
            IsActive = request.IsActive
        };

        _context.Staff.Add(staff);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddStaffMemberResponse(
            staff.Id,
            user.Id,
            user.Person.FirstName,
            user.Person.LastName,
            staff.Role,
            staff.IsActive
        );
    }
}