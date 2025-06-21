using MediatR;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;

namespace myschedule360.Application.Features.Inventory.Commands;

public class AddInventoryItemCommandHandler : IRequestHandler<AddInventoryItemCommand, AddInventoryItemResponse>
{
    private readonly IApplicationDbContext _context;

    public AddInventoryItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AddInventoryItemResponse> Handle(AddInventoryItemCommand request, CancellationToken cancellationToken)
    {
        var item = new InventoryItem
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Quantity = request.Quantity,
            MinimumStock = request.MinimumStock,
            BusinessId = request.BusinessId
        };

        _context.InventoryItems.Add(item);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddInventoryItemResponse(
            item.Id,
            item.Name,
            item.Price,
            item.Quantity,
            item.MinimumStock
        );
    }
}