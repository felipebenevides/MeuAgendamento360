using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Appointments.Commands;

public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, UpdateAppointmentResponse>
{
    private readonly IApplicationDbContext _context;

    public UpdateAppointmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateAppointmentResponse> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Service)
            .FirstOrDefaultAsync(a => a.Id == request.AppointmentId, cancellationToken);

        if (appointment == null)
            throw new InvalidOperationException("Appointment not found");

        // Check for conflicts if time is changing
        if (appointment.ScheduledAt != request.ScheduledAt)
        {
            var endTime = request.ScheduledAt.AddMinutes(appointment.Service.DurationMinutes);
            var hasConflict = await _context.Appointments
                .AnyAsync(a => a.Id != request.AppointmentId &&
                              a.StaffId == appointment.StaffId && 
                              a.ScheduledAt < endTime && 
                              a.ScheduledAt.AddMinutes(a.Service.DurationMinutes) > request.ScheduledAt &&
                              a.Status != AppointmentStatus.Cancelled, cancellationToken);

            if (hasConflict)
                throw new InvalidOperationException("Staff is not available at the requested time");
        }

        appointment.ScheduledAt = request.ScheduledAt;
        appointment.Notes = request.Notes;
        appointment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateAppointmentResponse(
            appointment.Id,
            appointment.ScheduledAt,
            appointment.Notes,
            appointment.UpdatedAt.Value
        );
    }
}