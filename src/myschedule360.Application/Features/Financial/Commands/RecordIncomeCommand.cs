using MediatR;

namespace myschedule360.Application.Features.Financial.Commands;

public record RecordIncomeCommand(
    decimal Amount,
    string Description,
    Guid BusinessId,
    Guid? AppointmentId = null,
    DateTime? TransactionDate = null
) : IRequest<RecordIncomeResponse>;

public record RecordIncomeResponse(
    Guid TransactionId,
    decimal Amount,
    string Description,
    DateTime TransactionDate
);