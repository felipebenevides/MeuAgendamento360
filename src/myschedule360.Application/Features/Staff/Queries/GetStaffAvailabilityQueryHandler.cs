using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Staff.Queries;

public class GetStaffAvailabilityQueryHandler : IRequestHandler<GetStaffAvailabilityQuery, GetStaffAvailabilityResponse>
{
    private readonly IApplicationDbContext _context;

    public GetStaffAvailabilityQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetStaffAvailabilityResponse> Handle(GetStaffAvailabilityQuery request, CancellationToken cancellationToken)
    {
        var availabilities = await _context.StaffAvailabilities
            .Where(sa => sa.StaffId == request.StaffId)
            .Select(sa => new StaffAvailabilityDto(
                sa.DayOfWeek,
                sa.StartTime,
                sa.EndTime,
                sa.IsAvailable
            ))
            .ToArrayAsync(cancellationToken);

        return new GetStaffAvailabilityResponse(
            request.StaffId,
            availabilities
        );
    }
}