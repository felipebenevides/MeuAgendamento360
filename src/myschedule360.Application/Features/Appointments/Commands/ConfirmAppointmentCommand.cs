using MediatR;

namespace myschedule360.Application.Features.Appointments.Commands;

public record ConfirmAppointmentCommand(Guid AppointmentId) : IRequest<ConfirmAppointmentResponse>;

public record ConfirmAppointmentResponse(
    Guid AppointmentId,
    string Status,
    DateTime ConfirmedAt
);