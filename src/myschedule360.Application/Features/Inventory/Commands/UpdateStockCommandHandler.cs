using MediatR;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Inventory.Commands;

public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, UpdateStockResponse>
{
    private readonly IApplicationDbContext _context;

    public UpdateStockCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateStockResponse> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.InventoryItems
            .FirstOrDefaultAsync(i => i.Id == request.ItemId, cancellationToken);

        if (item == null)
            throw new InvalidOperationException("Inventory item not found");

        var newQuantity = item.Quantity + request.QuantityChange;
        if (newQuantity < 0)
            throw new InvalidOperationException("Insufficient stock");

        item.Quantity = newQuantity;
        item.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateStockResponse(
            item.Id,
            item.Quantity,
            item.Quantity <= item.MinimumStock
        );
    }
}