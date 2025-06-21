using MediatR;

namespace myschedule360.Application.Features.Commission.Commands;

public record CalculateCommissionCommand(
    Guid AppointmentId
) : IRequest<CalculateCommissionResponse>;

public record CalculateCommissionResponse(
    Guid AppointmentId,
    Guid StaffId,
    decimal ServicePrice,
    decimal CommissionRate,
    decimal CommissionAmount
);