using MediatR;
using Microsoft.AspNetCore.Mvc;
using myschedule360.Application.Features.Services.Commands;
using myschedule360.Application.Features.Services.Queries;

namespace myschedule360.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CreateServiceResponse>> Create(CreateServiceCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{serviceId}")]
    public async Task<ActionResult<UpdateServiceResponse>> Update(Guid serviceId, UpdateServiceRequest request)
    {
        try
        {
            var command = new UpdateServiceCommand(serviceId, request.Name, request.Description, request.Price, request.DurationMinutes);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("{serviceId}/toggle-status")]
    public async Task<ActionResult<ToggleServiceStatusResponse>> ToggleStatus(Guid serviceId)
    {
        try
        {
            var result = await _mediator.Send(new ToggleServiceStatusCommand(serviceId));
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{serviceId}")]
    public async Task<ActionResult<DeleteServiceResponse>> Delete(Guid serviceId)
    {
        var result = await _mediator.Send(new DeleteServiceCommand(serviceId));
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpGet("business/{businessId}")]
    public async Task<ActionResult<GetServicesResponse>> GetByBusiness(Guid businessId, [FromQuery] bool? isActive = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetServicesQuery(businessId, isActive, page, pageSize));
        return Ok(result);
    }

    [HttpGet("{serviceId}")]
    public async Task<ActionResult<GetServiceByIdResponse>> GetById(Guid serviceId)
    {
        try
        {
            var result = await _mediator.Send(new GetServiceByIdQuery(serviceId));
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}

public record UpdateServiceRequest(
    string Name,
    string? Description,
    decimal Price,
    int DurationMinutes
);