using MediatR;

namespace myschedule360.Application.Features.Inventory.Queries;

public record GetInventoryItemsQuery(
    Guid BusinessId,
    bool? LowStockOnly = null
) : IRequest<GetInventoryItemsResponse>;

public record GetInventoryItemsResponse(
    InventoryItemDto[] Items
);

public record InventoryItemDto(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    int Quantity,
    int MinimumStock,
    bool IsLowStock,
    DateTime CreatedAt
);