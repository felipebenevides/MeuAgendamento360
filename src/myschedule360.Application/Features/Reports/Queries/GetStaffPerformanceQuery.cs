using MediatR;

namespace myschedule360.Application.Features.Reports.Queries;

public record GetStaffPerformanceQuery(
    Guid BusinessId,
    DateTime StartDate,
    DateTime EndDate
) : IRequest<GetStaffPerformanceResponse>;

public record GetStaffPerformanceResponse(
    StaffPerformanceDto[] StaffPerformance
);

public record StaffPerformanceDto(
    Guid StaffId,
    string StaffName,
    int TotalAppointments,
    int CompletedAppointments,
    int CancelledAppointments,
    decimal TotalRevenue,
    decimal CompletionRate,
    decimal AverageRating
);