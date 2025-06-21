using MediatR;

namespace myschedule360.Application.Features.Financial.Queries;

public record GetIncomeReportQuery(
    Guid BusinessId,
    DateTime StartDate,
    DateTime EndDate
) : IRequest<GetIncomeReportResponse>;

public record GetIncomeReportResponse(
    decimal TotalIncome,
    IncomeByServiceDto[] IncomeByService,
    IncomeByDayDto[] IncomeByDay
);

public record IncomeByServiceDto(
    string ServiceName,
    int AppointmentCount,
    decimal TotalIncome
);

public record IncomeByDayDto(
    DateTime Date,
    decimal Income
);