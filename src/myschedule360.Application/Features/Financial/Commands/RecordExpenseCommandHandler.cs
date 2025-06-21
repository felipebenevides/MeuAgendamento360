using MediatR;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Financial.Commands;

public class RecordExpenseCommandHandler : IRequestHandler<RecordExpenseCommand, RecordExpenseResponse>
{
    private readonly IApplicationDbContext _context;

    public RecordExpenseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RecordExpenseResponse> Handle(RecordExpenseCommand request, CancellationToken cancellationToken)
    {
        var transaction = new FinancialTransaction
        {
            Id = Guid.NewGuid(),
            Amount = request.Amount,
            Description = request.Description,
            Category = request.Category,
            Type = TransactionType.Expense,
            TransactionDate = request.TransactionDate ?? DateTime.UtcNow,
            BusinessId = request.BusinessId
        };

        _context.FinancialTransactions.Add(transaction);
        await _context.SaveChangesAsync(cancellationToken);

        return new RecordExpenseResponse(
            transaction.Id,
            transaction.Amount,
            transaction.Description,
            transaction.Category,
            transaction.TransactionDate
        );
    }
}