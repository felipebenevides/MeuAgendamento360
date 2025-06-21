using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Staff.Commands;

public class UpdateStaffMemberCommandHandler : IRequestHandler<UpdateStaffMemberCommand, UpdateStaffMemberResponse>
{
    private readonly IApplicationDbContext _context;

    public UpdateStaffMemberCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateStaffMemberResponse> Handle(UpdateStaffMemberCommand request, CancellationToken cancellationToken)
    {
        var staff = await _context.Staff
            .FirstOrDefaultAsync(s => s.Id == request.StaffId, cancellationToken);

        if (staff == null)
            throw new InvalidOperationException("Staff member not found");

        staff.Role = request.Role;
        staff.CommissionRate = request.CommissionRate;
        staff.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateStaffMemberResponse(
            staff.Id,
            staff.Role,
            staff.CommissionRate,
            staff.UpdatedAt.Value
        );
    }
}