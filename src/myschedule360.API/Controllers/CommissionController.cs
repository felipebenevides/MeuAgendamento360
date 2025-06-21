using MediatR;
using Microsoft.AspNetCore.Mvc;
using myschedule360.Application.Features.Commission.Commands;
using myschedule360.Application.Features.Commission.Queries;

namespace myschedule360.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CommissionController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommissionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("calculate/{appointmentId}")]
    public async Task<ActionResult<CalculateCommissionResponse>> Calculate(Guid appointmentId)
    {
        try
        {
            var result = await _mediator.Send(new CalculateCommissionCommand(appointmentId));
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("report/business/{businessId}")]
    public async Task<ActionResult<GetCommissionReportResponse>> GetReport(
        Guid businessId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        [FromQuery] Guid? staffId = null)
    {
        var result = await _mediator.Send(new GetCommissionReportQuery(businessId, startDate, endDate, staffId));
        return Ok(result);
    }
}