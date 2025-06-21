using MediatR;

namespace myschedule360.Application.Features.Notifications.Commands;

public record SendAppointmentReminderCommand(
    Guid AppointmentId
) : IRequest<SendAppointmentReminderResponse>;

public record SendAppointmentReminderResponse(
    Guid AppointmentId,
    string CustomerEmail,
    bool EmailSent,
    DateTime SentAt
);