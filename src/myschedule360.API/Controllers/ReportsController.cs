using MediatR;
using Microsoft.AspNetCore.Mvc;
using myschedule360.Application.Features.Reports.Queries;

namespace myschedule360.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("dashboard/business/{businessId}")]
    public async Task<ActionResult<GetBusinessDashboardResponse>> GetDashboard(
        Guid businessId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        var result = await _mediator.Send(new GetBusinessDashboardQuery(businessId, startDate, endDate));
        return Ok(result);
    }

    [HttpGet("staff-performance/business/{businessId}")]
    public async Task<ActionResult<GetStaffPerformanceResponse>> GetStaffPerformance(
        Guid businessId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        var result = await _mediator.Send(new GetStaffPerformanceQuery(businessId, startDate, endDate));
        return Ok(result);
    }
}