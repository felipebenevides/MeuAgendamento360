using MediatR;

namespace myschedule360.Application.Features.Commission.Queries;

public record GetCommissionReportQuery(
    Guid BusinessId,
    DateTime StartDate,
    DateTime EndDate,
    Guid? StaffId = null
) : IRequest<GetCommissionReportResponse>;

public record GetCommissionReportResponse(
    CommissionReportDto[] StaffCommissions,
    decimal TotalCommissions
);

public record CommissionReportDto(
    Guid StaffId,
    string StaffName,
    int AppointmentCount,
    decimal TotalRevenue,
    decimal TotalCommission
);