using MediatR;

namespace myschedule360.Application.Features.Appointments.Queries;

public record GetAvailableTimeSlotsQuery(
    Guid StaffId,
    Guid ServiceId,
    DateTime Date
) : IRequest<GetAvailableTimeSlotsResponse>;

public record GetAvailableTimeSlotsResponse(
    Guid StaffId,
    Guid ServiceId,
    DateTime Date,
    TimeSlotDto[] AvailableSlots
);

public record TimeSlotDto(
    DateTime StartTime,
    DateTime EndTime,
    bool IsAvailable
);