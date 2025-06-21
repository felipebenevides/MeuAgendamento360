using MediatR;
using Microsoft.AspNetCore.Mvc;
using myschedule360.Application.Features.Inventory.Commands;
using myschedule360.Application.Features.Inventory.Queries;

namespace myschedule360.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<AddInventoryItemResponse>> AddItem(AddInventoryItemCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("{itemId}/stock")]
    public async Task<ActionResult<UpdateStockResponse>> UpdateStock(Guid itemId, UpdateStockRequest request)
    {
        try
        {
            var command = new UpdateStockCommand(itemId, request.QuantityChange, request.MovementType, request.Notes);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("business/{businessId}")]
    public async Task<ActionResult<GetInventoryItemsResponse>> GetByBusiness(Guid businessId, [FromQuery] bool? lowStockOnly = null)
    {
        var result = await _mediator.Send(new GetInventoryItemsQuery(businessId, lowStockOnly));
        return Ok(result);
    }
}

public record UpdateStockRequest(
    int QuantityChange,
    string MovementType,
    string? Notes
);