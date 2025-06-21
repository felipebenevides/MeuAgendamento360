using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Notifications.Commands;

public class SendAppointmentConfirmationCommandHandler : IRequestHandler<SendAppointmentConfirmationCommand, SendAppointmentConfirmationResponse>
{
    private readonly IApplicationDbContext _context;

    public SendAppointmentConfirmationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SendAppointmentConfirmationResponse> Handle(SendAppointmentConfirmationCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Customer)
            .ThenInclude(c => c.Person)
            .Include(a => a.Service)
            .Include(a => a.Business)
            .FirstOrDefaultAsync(a => a.Id == request.AppointmentId, cancellationToken);

        if (appointment == null)
            throw new InvalidOperationException("Appointment not found");

        // Simulate email sending
        var emailSent = true;
        var sentAt = DateTime.UtcNow;

        return new SendAppointmentConfirmationResponse(
            appointment.Id,
            appointment.Customer.Person.Email,
            emailSent,
            sentAt
        );
    }
}