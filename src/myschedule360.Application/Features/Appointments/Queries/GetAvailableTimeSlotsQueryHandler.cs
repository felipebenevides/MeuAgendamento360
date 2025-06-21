using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Appointments.Queries;

public class GetAvailableTimeSlotsQueryHandler : IRequestHandler<GetAvailableTimeSlotsQuery, GetAvailableTimeSlotsResponse>
{
    private readonly IApplicationDbContext _context;

    public GetAvailableTimeSlotsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetAvailableTimeSlotsResponse> Handle(GetAvailableTimeSlotsQuery request, CancellationToken cancellationToken)
    {
        // Get service duration
        var service = await _context.Services.FirstOrDefaultAsync(s => s.Id == request.ServiceId, cancellationToken);
        if (service == null) throw new InvalidOperationException("Service not found");

        // Get staff availability for the day
        var dayOfWeek = request.Date.DayOfWeek;
        var availability = await _context.StaffAvailabilities
            .FirstOrDefaultAsync(sa => sa.StaffId == request.StaffId && sa.DayOfWeek == dayOfWeek && sa.IsAvailable, cancellationToken);

        if (availability == null)
        {
            return new GetAvailableTimeSlotsResponse(request.StaffId, request.ServiceId, request.Date, Array.Empty<TimeSlotDto>());
        }

        // Get existing appointments for the day
        var startOfDay = request.Date.Date;
        var endOfDay = startOfDay.AddDays(1);
        var appointments = await _context.Appointments
            .Where(a => a.StaffId == request.StaffId && 
                       a.ScheduledAt >= startOfDay && 
                       a.ScheduledAt < endOfDay &&
                       a.Status != AppointmentStatus.Cancelled)
            .Select(a => new { a.ScheduledAt, a.DurationMinutes })
            .ToListAsync(cancellationToken);

        // Generate time slots
        var slots = new List<TimeSlotDto>();
        var workStart = startOfDay.Add(availability.StartTime.ToTimeSpan());
        var workEnd = startOfDay.Add(availability.EndTime.ToTimeSpan());
        var slotDuration = TimeSpan.FromMinutes(service.DurationMinutes);

        for (var time = workStart; time.Add(slotDuration) <= workEnd; time = time.AddMinutes(30))
        {
            var slotEnd = time.Add(slotDuration);
            var isAvailable = !appointments.Any(a => 
                time < a.ScheduledAt.AddMinutes(a.DurationMinutes) && 
                slotEnd > a.ScheduledAt);

            slots.Add(new TimeSlotDto(time, slotEnd, isAvailable));
        }

        return new GetAvailableTimeSlotsResponse(
            request.StaffId,
            request.ServiceId,
            request.Date,
            slots.ToArray()
        );
    }
}