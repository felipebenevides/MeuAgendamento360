using MediatR;
using Microsoft.AspNetCore.Mvc;
using myschedule360.Application.Features.Staff.Commands;
using myschedule360.Application.Features.Staff.Queries;

namespace myschedule360.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class StaffController : ControllerBase
{
    private readonly IMediator _mediator;

    public StaffController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<AddStaffMemberResponse>> AddStaffMember(AddStaffMemberCommand command)
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

    [HttpPut("{staffId}")]
    public async Task<ActionResult<UpdateStaffMemberResponse>> UpdateStaffMember(Guid staffId, UpdateStaffMemberRequest request)
    {
        try
        {
            var command = new UpdateStaffMemberCommand(staffId, request.Role, request.CommissionRate);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{staffId}")]
    public async Task<ActionResult<RemoveStaffMemberResponse>> RemoveStaffMember(Guid staffId)
    {
        var result = await _mediator.Send(new RemoveStaffMemberCommand(staffId));
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpGet("business/{businessId}")]
    public async Task<ActionResult<GetStaffMembersResponse>> GetStaffMembers(Guid businessId, [FromQuery] bool? isActive = null)
    {
        var result = await _mediator.Send(new GetStaffMembersQuery(businessId, isActive));
        return Ok(result);
    }

    [HttpPost("{staffId}/availability")]
    public async Task<ActionResult<SetStaffAvailabilityResponse>> SetAvailability(Guid staffId, SetAvailabilityRequest request)
    {
        try
        {
            var command = new SetStaffAvailabilityCommand(staffId, request.DayOfWeek, request.StartTime, request.EndTime, request.IsAvailable);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("{staffId}/availability")]
    public async Task<ActionResult<GetStaffAvailabilityResponse>> GetAvailability(Guid staffId)
    {
        var result = await _mediator.Send(new GetStaffAvailabilityQuery(staffId));
        return Ok(result);
    }

    [HttpGet("{staffId}/schedule")]
    public async Task<ActionResult<GetStaffScheduleResponse>> GetSchedule(Guid staffId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var result = await _mediator.Send(new GetStaffScheduleQuery(staffId, startDate, endDate));
        return Ok(result);
    }
}

public record UpdateStaffMemberRequest(
    string Role,
    decimal? CommissionRate
);

public record SetAvailabilityRequest(
    DayOfWeek DayOfWeek,
    TimeOnly StartTime,
    TimeOnly EndTime,
    bool IsAvailable
);