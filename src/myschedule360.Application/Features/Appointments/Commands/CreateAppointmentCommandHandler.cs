using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Appointments.Commands;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, CreateAppointmentResponse>
{
    private readonly IApplicationDbContext _context;

    public CreateAppointmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CreateAppointmentResponse> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        // Validate entities exist
        var service = await _context.Services.FirstOrDefaultAsync(s => s.Id == request.ServiceId, cancellationToken);
        if (service == null) throw new InvalidOperationException("Service not found");

        var customer = await _context.Customers.Include(c => c.Person).FirstOrDefaultAsync(c => c.Id == request.CustomerId, cancellationToken);
        if (customer == null) throw new InvalidOperationException("Customer not found");

        var staff = await _context.Staff.Include(s => s.User).ThenInclude(u => u.Person).FirstOrDefaultAsync(s => s.Id == request.StaffId, cancellationToken);
        if (staff == null) throw new InvalidOperationException("Staff not found");

        // Check for conflicts
        var endTime = request.ScheduledAt.AddMinutes(service.DurationMinutes);
        var hasConflict = await _context.Appointments
            .AnyAsync(a => a.StaffId == request.StaffId && 
                          a.ScheduledAt < endTime && 
                          a.ScheduledAt.AddMinutes(a.Service.DurationMinutes) > request.ScheduledAt &&
                          a.Status != AppointmentStatus.Cancelled, cancellationToken);

        if (hasConflict)
            throw new InvalidOperationException("Staff is not available at the requested time");

        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            ScheduledAt = request.ScheduledAt,
            DurationMinutes = service.DurationMinutes,
            Price = service.Price,
            Notes = request.Notes,
            CustomerId = request.CustomerId,
            ServiceId = request.ServiceId,
            StaffId = request.StaffId,
            BusinessId = request.BusinessId,
            Status = AppointmentStatus.Scheduled
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateAppointmentResponse(
            appointment.Id,
            appointment.ScheduledAt,
            service.Name,
            $"{customer.Person.FirstName} {customer.Person.LastName}",
            $"{staff.User.Person.FirstName} {staff.User.Person.LastName}",
            appointment.Price,
            appointment.Status.ToString()
        );
    }
}