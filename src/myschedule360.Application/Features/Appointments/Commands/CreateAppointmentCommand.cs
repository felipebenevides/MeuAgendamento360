using MediatR;

namespace myschedule360.Application.Features.Appointments.Commands;

public record CreateAppointmentCommand(
    DateTime ScheduledAt,
    Guid CustomerId,
    Guid ServiceId,
    Guid StaffId,
    Guid BusinessId,
    string? Notes
) : IRequest<CreateAppointmentResponse>;

public record CreateAppointmentResponse(
    Guid AppointmentId,
    DateTime ScheduledAt,
    string ServiceName,
    string CustomerName,
    string StaffName,
    decimal Price,
    string Status
);