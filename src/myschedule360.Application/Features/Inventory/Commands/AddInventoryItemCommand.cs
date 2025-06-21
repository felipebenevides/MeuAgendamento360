using MediatR;

namespace myschedule360.Application.Features.Inventory.Commands;

public record AddInventoryItemCommand(
    string Name,
    string? Description,
    decimal Price,
    int Quantity,
    int MinimumStock,
    Guid BusinessId
) : IRequest<AddInventoryItemResponse>;

public record AddInventoryItemResponse(
    Guid ItemId,
    string Name,
    decimal Price,
    int Quantity,
    int MinimumStock
);