using MediatR;

namespace myschedule360.Application.Features.Appointments.Queries;

public record GetAppointmentsQuery(
    Guid BusinessId,
    DateTime? StartDate = null,
    DateTime? EndDate = null,
    Guid? StaffId = null,
    string? Status = null,
    int Page = 1,
    int PageSize = 10
) : IRequest<GetAppointmentsResponse>;

public record GetAppointmentsResponse(
    AppointmentDto[] Appointments,
    int TotalCount,
    int Page,
    int PageSize
);

public record AppointmentDto(
    Guid Id,
    DateTime ScheduledAt,
    int DurationMinutes,
    string ServiceName,
    string CustomerName,
    string StaffName,
    decimal Price,
    string Status,
    string? Notes
);