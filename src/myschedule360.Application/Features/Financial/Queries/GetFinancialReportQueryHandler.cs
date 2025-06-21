using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Financial.Queries;

public class GetFinancialReportQueryHandler : IRequestHandler<GetFinancialReportQuery, GetFinancialReportResponse>
{
    private readonly IApplicationDbContext _context;

    public GetFinancialReportQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetFinancialReportResponse> Handle(GetFinancialReportQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _context.FinancialTransactions
            .Where(t => t.BusinessId == request.BusinessId &&
                       t.TransactionDate >= request.StartDate &&
                       t.TransactionDate <= request.EndDate)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync(cancellationToken);

        var totalIncome = transactions
            .Where(t => t.Type == TransactionType.Income)
            .Sum(t => t.Amount);

        var totalExpenses = transactions
            .Where(t => t.Type == TransactionType.Expense)
            .Sum(t => t.Amount);

        var transactionDtos = transactions
            .Select(t => new FinancialTransactionDto(
                t.Id,
                t.Amount,
                t.Description,
                t.Type.ToString(),
                t.Category,
                t.TransactionDate
            ))
            .ToArray();

        return new GetFinancialReportResponse(
            totalIncome,
            totalExpenses,
            totalIncome - totalExpenses,
            transactionDtos
        );
    }
}