using MediatR;
using Microsoft.AspNetCore.Mvc;
using myschedule360.Application.Features.Financial.Commands;
using myschedule360.Application.Features.Financial.Queries;

namespace myschedule360.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class FinancialController : ControllerBase
{
    private readonly IMediator _mediator;

    public FinancialController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("income")]
    public async Task<ActionResult<RecordIncomeResponse>> RecordIncome(RecordIncomeCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("expense")]
    public async Task<ActionResult<RecordExpenseResponse>> RecordExpense(RecordExpenseCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("report/business/{businessId}")]
    public async Task<ActionResult<GetFinancialReportResponse>> GetFinancialReport(
        Guid businessId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        var result = await _mediator.Send(new GetFinancialReportQuery(businessId, startDate, endDate));
        return Ok(result);
    }

    [HttpGet("income-report/business/{businessId}")]
    public async Task<ActionResult<GetIncomeReportResponse>> GetIncomeReport(
        Guid businessId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        var result = await _mediator.Send(new GetIncomeReportQuery(businessId, startDate, endDate));
        return Ok(result);
    }
}