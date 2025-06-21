using MediatR;

namespace myschedule360.Application.Features.Financial.Commands;

public record RecordExpenseCommand(
    decimal Amount,
    string Description,
    string Category,
    Guid BusinessId,
    DateTime? TransactionDate = null
) : IRequest<RecordExpenseResponse>;

public record RecordExpenseResponse(
    Guid TransactionId,
    decimal Amount,
    string Description,
    string Category,
    DateTime TransactionDate
);