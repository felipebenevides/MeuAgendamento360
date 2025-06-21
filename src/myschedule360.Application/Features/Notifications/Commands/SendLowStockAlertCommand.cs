using MediatR;

namespace myschedule360.Application.Features.Notifications.Commands;

public record SendLowStockAlertCommand(
    Guid BusinessId
) : IRequest<SendLowStockAlertResponse>;

public record SendLowStockAlertResponse(
    Guid BusinessId,
    int LowStockItemsCount,
    bool AlertSent,
    DateTime SentAt
);