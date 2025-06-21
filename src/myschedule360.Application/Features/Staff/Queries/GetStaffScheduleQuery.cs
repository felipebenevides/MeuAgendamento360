using MediatR;

namespace myschedule360.Application.Features.Staff.Queries;

public record GetStaffScheduleQuery(
    Guid StaffId,
    DateTime StartDate,
    DateTime EndDate
) : IRequest<GetStaffScheduleResponse>;

public record GetStaffScheduleResponse(
    Guid StaffId,
    DateTime StartDate,
    DateTime EndDate,
    StaffScheduleDto[] Schedule
);

public record StaffScheduleDto(
    DateTime Date,
    TimeOnly StartTime,
    TimeOnly EndTime,
    bool IsAvailable,
    AppointmentDto[] Appointments
);

public record AppointmentDto(
    Guid AppointmentId,
    DateTime DateTime,
    int DurationMinutes,
    string ServiceName,
    string CustomerName
);