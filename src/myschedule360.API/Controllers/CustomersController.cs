using MediatR;
using Microsoft.AspNetCore.Mvc;
using myschedule360.Application.Features.Customers.Commands;
using myschedule360.Application.Features.Customers.Queries;

namespace myschedule360.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<RegisterCustomerResponse>> Register(RegisterCustomerCommand command)
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

    [HttpPut("{customerId}")]
    public async Task<ActionResult<UpdateCustomerResponse>> Update(Guid customerId, UpdateCustomerRequest request)
    {
        try
        {
            var command = new UpdateCustomerCommand(customerId, request.FirstName, request.LastName, request.Phone, request.BirthDate, request.Notes);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("business/{businessId}")]
    public async Task<ActionResult<GetCustomersResponse>> GetByBusiness(Guid businessId, [FromQuery] string? searchTerm = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetCustomersQuery(businessId, searchTerm, page, pageSize));
        return Ok(result);
    }

    [HttpGet("{customerId}")]
    public async Task<ActionResult<GetCustomerByIdResponse>> GetById(Guid customerId)
    {
        try
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery(customerId));
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("{customerId}/appointments")]
    public async Task<ActionResult<GetCustomerAppointmentHistoryResponse>> GetAppointmentHistory(Guid customerId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetCustomerAppointmentHistoryQuery(customerId, page, pageSize));
        return Ok(result);
    }

    [HttpPost("{customerId}/notes")]
    public async Task<ActionResult<AddCustomerNotesResponse>> AddNotes(Guid customerId, AddNotesRequest request)
    {
        try
        {
            var command = new AddCustomerNotesCommand(customerId, request.Notes);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}

public record UpdateCustomerRequest(
    string FirstName,
    string LastName,
    string Phone,
    DateTime? BirthDate,
    string? Notes
);

public record AddNotesRequest(string Notes);