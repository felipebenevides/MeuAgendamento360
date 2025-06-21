using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Staff.Commands;

public class RemoveStaffMemberCommandHandler : IRequestHandler<RemoveStaffMemberCommand, RemoveStaffMemberResponse>
{
    private readonly IApplicationDbContext _context;

    public RemoveStaffMemberCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RemoveStaffMemberResponse> Handle(RemoveStaffMemberCommand request, CancellationToken cancellationToken)
    {
        var staff = await _context.Staff
            .FirstOrDefaultAsync(s => s.Id == request.StaffId, cancellationToken);

        if (staff == null)
            return new RemoveStaffMemberResponse(false, "Staff member not found");

        // Check for future appointments
        var hasFutureAppointments = await _context.Appointments
            .AnyAsync(a => a.StaffId == request.StaffId && a.ScheduledAt > DateTime.UtcNow, cancellationToken);

        if (hasFutureAppointments)
            return new RemoveStaffMemberResponse(false, "Cannot remove staff member with future appointments");

        _context.Staff.Remove(staff);
        await _context.SaveChangesAsync(cancellationToken);

        return new RemoveStaffMemberResponse(true, "Staff member removed successfully");
    }
}