using MediatR;

namespace myschedule360.Application.Features.Appointments.Commands;

public record UpdateAppointmentCommand(
    Guid AppointmentId,
    DateTime ScheduledAt,
    string? Notes
) : IRequest<UpdateAppointmentResponse>;

public record UpdateAppointmentResponse(
    Guid AppointmentId,
    DateTime ScheduledAt,
    string? Notes,
    DateTime UpdatedAt
);