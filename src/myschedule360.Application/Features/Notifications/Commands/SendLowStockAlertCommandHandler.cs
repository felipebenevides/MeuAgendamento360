using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Notifications.Commands;

public class SendLowStockAlertCommandHandler : IRequestHandler<SendLowStockAlertCommand, SendLowStockAlertResponse>
{
    private readonly IApplicationDbContext _context;

    public SendLowStockAlertCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SendLowStockAlertResponse> Handle(SendLowStockAlertCommand request, CancellationToken cancellationToken)
    {
        var lowStockItems = await _context.InventoryItems
            .Where(i => i.BusinessId == request.BusinessId && i.Quantity <= i.MinimumStock)
            .CountAsync(cancellationToken);

        // Simulate alert sending
        var alertSent = lowStockItems > 0;
        var sentAt = DateTime.UtcNow;

        return new SendLowStockAlertResponse(
            request.BusinessId,
            lowStockItems,
            alertSent,
            sentAt
        );
    }
}