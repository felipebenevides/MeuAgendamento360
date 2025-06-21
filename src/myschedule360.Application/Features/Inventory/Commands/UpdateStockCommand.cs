using MediatR;

namespace myschedule360.Application.Features.Inventory.Commands;

public record UpdateStockCommand(
    Guid ItemId,
    int QuantityChange,
    string MovementType,
    string? Notes
) : IRequest<UpdateStockResponse>;

public record UpdateStockResponse(
    Guid ItemId,
    int NewQuantity,
    bool IsLowStock
);