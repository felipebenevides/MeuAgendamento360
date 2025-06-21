using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Staff.Queries;

public class GetStaffScheduleQueryHandler : IRequestHandler<GetStaffScheduleQuery, GetStaffScheduleResponse>
{
    private readonly IApplicationDbContext _context;

    public GetStaffScheduleQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetStaffScheduleResponse> Handle(GetStaffScheduleQuery request, CancellationToken cancellationToken)
    {
        // Get staff availabilities
        var availabilities = await _context.StaffAvailabilities
            .Where(sa => sa.StaffId == request.StaffId)
            .ToListAsync(cancellationToken);

        // Get appointments in date range
        var appointments = await _context.Appointments
            .Include(a => a.Service)
            .Include(a => a.Customer)
            .ThenInclude(c => c.Person)
            .Where(a => a.StaffId == request.StaffId && 
                       a.ScheduledAt >= request.StartDate && 
                       a.ScheduledAt <= request.EndDate)
            .ToListAsync(cancellationToken);

        var schedule = new List<StaffScheduleDto>();
        var currentDate = request.StartDate.Date;

        while (currentDate <= request.EndDate.Date)
        {
            var dayOfWeek = currentDate.DayOfWeek;
            var availability = availabilities.FirstOrDefault(a => a.DayOfWeek == dayOfWeek);
            
            var dayAppointments = appointments
                .Where(a => a.ScheduledAt.Date == currentDate)
                .Select(a => new AppointmentDto(
                    a.Id,
                    a.ScheduledAt,
                    a.Service.DurationMinutes,
                    a.Service.Name,
                    $"{a.Customer.Person.FirstName} {a.Customer.Person.LastName}"
                ))
                .ToArray();

            if (availability != null)
            {
                schedule.Add(new StaffScheduleDto(
                    currentDate,
                    availability.StartTime,
                    availability.EndTime,
                    availability.IsAvailable,
                    dayAppointments
                ));
            }

            currentDate = currentDate.AddDays(1);
        }

        return new GetStaffScheduleResponse(
            request.StaffId,
            request.StartDate,
            request.EndDate,
            schedule.ToArray()
        );
    }
}