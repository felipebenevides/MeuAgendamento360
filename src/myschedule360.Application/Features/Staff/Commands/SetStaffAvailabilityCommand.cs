using MediatR;

namespace myschedule360.Application.Features.Staff.Commands;

public record SetStaffAvailabilityCommand(
    Guid StaffId,
    DayOfWeek DayOfWeek,
    TimeOnly StartTime,
    TimeOnly EndTime,
    bool IsAvailable
) : IRequest<SetStaffAvailabilityResponse>;

public record SetStaffAvailabilityResponse(
    Guid StaffId,
    DayOfWeek DayOfWeek,
    TimeOnly StartTime,
    TimeOnly EndTime,
    bool IsAvailable
);