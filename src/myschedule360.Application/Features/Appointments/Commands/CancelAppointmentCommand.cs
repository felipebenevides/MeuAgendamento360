using MediatR;

namespace myschedule360.Application.Features.Appointments.Commands;

public record CancelAppointmentCommand(
    Guid AppointmentId,
    string? CancellationReason
) : IRequest<CancelAppointmentResponse>;

public record CancelAppointmentResponse(
    Guid AppointmentId,
    string Status,
    DateTime CancelledAt
);