using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Appointments.Commands;

public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, CancelAppointmentResponse>
{
    private readonly IApplicationDbContext _context;

    public CancelAppointmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CancelAppointmentResponse> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _context.Appointments
            .FirstOrDefaultAsync(a => a.Id == request.AppointmentId, cancellationToken);

        if (appointment == null)
            throw new InvalidOperationException("Appointment not found");

        if (appointment.Status == AppointmentStatus.Cancelled)
            throw new InvalidOperationException("Appointment is already cancelled");

        appointment.Status = AppointmentStatus.Cancelled;
        appointment.Notes = string.IsNullOrEmpty(appointment.Notes) 
            ? $"Cancelled: {request.CancellationReason}"
            : $"{appointment.Notes}\nCancelled: {request.CancellationReason}";
        appointment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new CancelAppointmentResponse(
            appointment.Id,
            appointment.Status.ToString(),
            appointment.UpdatedAt.Value
        );
    }
}