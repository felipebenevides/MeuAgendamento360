using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Staff.Commands;

public class SetStaffAvailabilityCommandHandler : IRequestHandler<SetStaffAvailabilityCommand, SetStaffAvailabilityResponse>
{
    private readonly IApplicationDbContext _context;

    public SetStaffAvailabilityCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SetStaffAvailabilityResponse> Handle(SetStaffAvailabilityCommand request, CancellationToken cancellationToken)
    {
        // Verify staff exists
        if (!await _context.Staff.AnyAsync(s => s.Id == request.StaffId, cancellationToken))
            throw new InvalidOperationException("Staff member not found");

        // Find existing availability for this day
        var availability = await _context.StaffAvailabilities
            .FirstOrDefaultAsync(sa => sa.StaffId == request.StaffId && sa.DayOfWeek == request.DayOfWeek, cancellationToken);

        if (availability == null)
        {
            // Create new availability
            availability = new StaffAvailability
            {
                Id = Guid.NewGuid(),
                StaffId = request.StaffId,
                DayOfWeek = request.DayOfWeek,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                IsAvailable = request.IsAvailable
            };
            _context.StaffAvailabilities.Add(availability);
        }
        else
        {
            // Update existing availability
            availability.StartTime = request.StartTime;
            availability.EndTime = request.EndTime;
            availability.IsAvailable = request.IsAvailable;
            availability.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new SetStaffAvailabilityResponse(
            availability.StaffId,
            availability.DayOfWeek,
            availability.StartTime,
            availability.EndTime,
            availability.IsAvailable
        );
    }
}