using MediatR;

namespace myschedule360.Application.Features.Reports.Queries;

public record GetBusinessDashboardQuery(
    Guid BusinessId,
    DateTime StartDate,
    DateTime EndDate
) : IRequest<GetBusinessDashboardResponse>;

public record GetBusinessDashboardResponse(
    int TotalAppointments,
    decimal TotalRevenue,
    int TotalCustomers,
    int ActiveStaff,
    AppointmentStatusSummaryDto AppointmentsByStatus,
    TopServiceDto[] TopServices,
    RevenueByDayDto[] RevenueByDay
);

public record AppointmentStatusSummaryDto(
    int Scheduled,
    int Confirmed,
    int Completed,
    int Cancelled,
    int NoShow
);

public record TopServiceDto(
    string ServiceName,
    int AppointmentCount,
    decimal Revenue
);

public record RevenueByDayDto(
    DateTime Date,
    decimal Revenue,
    int AppointmentCount
);