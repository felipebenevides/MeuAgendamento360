using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Inventory.Queries;

public class GetInventoryItemsQueryHandler : IRequestHandler<GetInventoryItemsQuery, GetInventoryItemsResponse>
{
    private readonly IApplicationDbContext _context;

    public GetInventoryItemsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetInventoryItemsResponse> Handle(GetInventoryItemsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.InventoryItems
            .Where(i => i.BusinessId == request.BusinessId);

        if (request.LowStockOnly == true)
            query = query.Where(i => i.Quantity <= i.MinimumStock);

        var items = await query
            .OrderBy(i => i.Name)
            .Select(i => new InventoryItemDto(
                i.Id,
                i.Name,
                i.Description,
                i.Price,
                i.Quantity,
                i.MinimumStock,
                i.Quantity <= i.MinimumStock,
                i.CreatedAt
            ))
            .ToArrayAsync(cancellationToken);

        return new GetInventoryItemsResponse(items);
    }
}