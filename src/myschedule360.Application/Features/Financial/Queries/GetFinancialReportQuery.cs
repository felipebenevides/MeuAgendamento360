using MediatR;

namespace myschedule360.Application.Features.Financial.Queries;

public record GetFinancialReportQuery(
    Guid BusinessId,
    DateTime StartDate,
    DateTime EndDate
) : IRequest<GetFinancialReportResponse>;

public record GetFinancialReportResponse(
    decimal TotalIncome,
    decimal TotalExpenses,
    decimal NetProfit,
    FinancialTransactionDto[] Transactions
);

public record FinancialTransactionDto(
    Guid Id,
    decimal Amount,
    string Description,
    string Type,
    string? Category,
    DateTime TransactionDate
);