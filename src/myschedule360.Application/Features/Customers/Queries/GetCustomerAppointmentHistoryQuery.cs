using MediatR;

namespace myschedule360.Application.Features.Customers.Queries;

public record GetCustomerAppointmentHistoryQuery(
    Guid CustomerId,
    int Page = 1,
    int PageSize = 10
) : IRequest<GetCustomerAppointmentHistoryResponse>;

public record GetCustomerAppointmentHistoryResponse(
    Guid CustomerId,
    CustomerAppointmentDto[] Appointments,
    int TotalCount,
    int Page,
    int PageSize
);

public record CustomerAppointmentDto(
    Guid AppointmentId,
    DateTime ScheduledAt,
    string ServiceName,
    string StaffName,
    decimal Price,
    string Status,
    string? Notes
);