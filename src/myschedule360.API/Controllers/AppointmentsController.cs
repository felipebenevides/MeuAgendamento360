using MediatR;
using Microsoft.AspNetCore.Mvc;
using myschedule360.Application.Features.Appointments.Commands;
using myschedule360.Application.Features.Appointments.Queries;

namespace myschedule360.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CreateAppointmentResponse>> Create(CreateAppointmentCommand command)
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

    [HttpPut("{appointmentId}")]
    public async Task<ActionResult<UpdateAppointmentResponse>> Update(Guid appointmentId, UpdateAppointmentRequest request)
    {
        try
        {
            var command = new UpdateAppointmentCommand(appointmentId, request.ScheduledAt, request.Notes);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("{appointmentId}/cancel")]
    public async Task<ActionResult<CancelAppointmentResponse>> Cancel(Guid appointmentId, CancelAppointmentRequest request)
    {
        try
        {
            var command = new CancelAppointmentCommand(appointmentId, request.CancellationReason);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("{appointmentId}/confirm")]
    public async Task<ActionResult<ConfirmAppointmentResponse>> Confirm(Guid appointmentId)
    {
        try
        {
            var result = await _mediator.Send(new ConfirmAppointmentCommand(appointmentId));
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("business/{businessId}")]
    public async Task<ActionResult<GetAppointmentsResponse>> GetByBusiness(
        Guid businessId,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] Guid? staffId = null,
        [FromQuery] string? status = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAppointmentsQuery(businessId, startDate, endDate, staffId, status, page, pageSize));
        return Ok(result);
    }

    [HttpGet("available-slots")]
    public async Task<ActionResult<GetAvailableTimeSlotsResponse>> GetAvailableSlots(
        [FromQuery] Guid staffId,
        [FromQuery] Guid serviceId,
        [FromQuery] DateTime date)
    {
        try
        {
            var result = await _mediator.Send(new GetAvailableTimeSlotsQuery(staffId, serviceId, date));
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

public record UpdateAppointmentRequest(
    DateTime ScheduledAt,
    string? Notes
);

public record CancelAppointmentRequest(string? CancellationReason);