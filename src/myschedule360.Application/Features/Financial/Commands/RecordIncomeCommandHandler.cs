using MediatR;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Financial.Commands;

public class RecordIncomeCommandHandler : IRequestHandler<RecordIncomeCommand, RecordIncomeResponse>
{
    private readonly IApplicationDbContext _context;

    public RecordIncomeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RecordIncomeResponse> Handle(RecordIncomeCommand request, CancellationToken cancellationToken)
    {
        var transaction = new FinancialTransaction
        {
            Id = Guid.NewGuid(),
            Amount = request.Amount,
            Description = request.Description,
            Type = TransactionType.Income,
            TransactionDate = request.TransactionDate ?? DateTime.UtcNow,
            BusinessId = request.BusinessId,
            AppointmentId = request.AppointmentId
        };

        _context.FinancialTransactions.Add(transaction);
        await _context.SaveChangesAsync(cancellationToken);

        return new RecordIncomeResponse(
            transaction.Id,
            transaction.Amount,
            transaction.Description,
            transaction.TransactionDate
        );
    }
}