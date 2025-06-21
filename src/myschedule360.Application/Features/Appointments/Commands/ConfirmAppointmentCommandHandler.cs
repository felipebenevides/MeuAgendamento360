using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Appointments.Commands;

public class ConfirmAppointmentCommandHandler : IRequestHandler<ConfirmAppointmentCommand, ConfirmAppointmentResponse>
{
    private readonly IApplicationDbContext _context;

    public ConfirmAppointmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ConfirmAppointmentResponse> Handle(ConfirmAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _context.Appointments
            .FirstOrDefaultAsync(a => a.Id == request.AppointmentId, cancellationToken);

        if (appointment == null)
            throw new InvalidOperationException("Appointment not found");

        if (appointment.Status != AppointmentStatus.Scheduled)
            throw new InvalidOperationException("Only scheduled appointments can be confirmed");

        appointment.Status = AppointmentStatus.Confirmed;
        appointment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new ConfirmAppointmentResponse(
            appointment.Id,
            appointment.Status.ToString(),
            appointment.UpdatedAt.Value
        );
    }
}