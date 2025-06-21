using MediatR;
using Microsoft.AspNetCore.Mvc;
using myschedule360.Application.Features.BusinessOnboarding.Commands;
using myschedule360.Application.Features.BusinessOnboarding.Queries;

namespace myschedule360.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BusinessController : ControllerBase
{
    private readonly IMediator _mediator;

    public BusinessController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("setup")]
    public async Task<ActionResult<CreateBusinessResponse>> Setup(CreateBusinessCommand command)
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

    [HttpPut("info")]
    public async Task<ActionResult<UpdateBusinessInfoResponse>> UpdateInfo(UpdateBusinessInfoCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("{businessId}/complete-onboarding")]
    public async Task<ActionResult<CompleteOnboardingResponse>> CompleteOnboarding(Guid businessId)
    {
        try
        {
            var result = await _mediator.Send(new CompleteOnboardingCommand(businessId));
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("{businessId}/setup-status")]
    public async Task<ActionResult<GetBusinessSetupStatusResponse>> GetSetupStatus(Guid businessId)
    {
        try
        {
            var result = await _mediator.Send(new GetBusinessSetupStatusQuery(businessId));
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("validate-slug")]
    public async Task<ActionResult<ValidateBusinessSlugResponse>> ValidateSlug([FromBody] ValidateSlugRequest request)
    {
        var result = await _mediator.Send(new ValidateBusinessSlugQuery(request.Slug));
        return Ok(result);
    }

    [HttpGet("{businessId}")]
    public async Task<ActionResult<GetBusinessInfoResponse>> GetBusinessInfo(Guid businessId)
    {
        try
        {
            var result = await _mediator.Send(new GetBusinessInfoQuery(businessId));
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}

public record ValidateSlugRequest(string Slug);