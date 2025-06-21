using MediatR;

namespace myschedule360.Application.Features.Notifications.Commands;

public record SendAppointmentConfirmationCommand(
    Guid AppointmentId
) : IRequest<SendAppointmentConfirmationResponse>;

public record SendAppointmentConfirmationResponse(
    Guid AppointmentId,
    string CustomerEmail,
    bool EmailSent,
    DateTime SentAt
);