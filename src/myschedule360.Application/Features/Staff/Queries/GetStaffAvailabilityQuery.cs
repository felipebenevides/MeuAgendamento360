using MediatR;

namespace myschedule360.Application.Features.Staff.Queries;

public record GetStaffAvailabilityQuery(Guid StaffId) : IRequest<GetStaffAvailabilityResponse>;

public record GetStaffAvailabilityResponse(
    Guid StaffId,
    StaffAvailabilityDto[] Availabilities
);

public record StaffAvailabilityDto(
    DayOfWeek DayOfWeek,
    TimeOnly StartTime,
    TimeOnly EndTime,
    bool IsAvailable
);