using MediatR;
using Microsoft.AspNetCore.Mvc;
using myschedule360.Application.Features.Authentication.Commands;
using myschedule360.Application.Features.Authentication.Queries;

namespace myschedule360.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterBusinessOwnerResponse>> Register(RegisterBusinessOwnerCommand command)
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

    [HttpPost("login")]
    public async Task<ActionResult<LoginUserResponse>> Login(LoginUserCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpGet("profile/{userId}")]
    public async Task<ActionResult<GetUserProfileResponse>> GetProfile(Guid userId)
    {
        try
        {
            var result = await _mediator.Send(new GetUserProfileQuery(userId));
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("add-service-provider")]
    public async Task<ActionResult<AddServiceProviderResponse>> AddServiceProvider(AddServiceProviderCommand command)
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

    [HttpPut("profile")]
    public async Task<ActionResult<UpdateUserProfileResponse>> UpdateProfile(UpdateUserProfileCommand command)
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

    [HttpGet("service-providers/{businessId}")]
    public async Task<ActionResult<GetServiceProvidersResponse>> GetServiceProviders(Guid businessId)
    {
        var result = await _mediator.Send(new GetServiceProvidersQuery(businessId));
        return Ok(result);
    }

    [HttpPost("request-password-reset")]
    public async Task<ActionResult<RequestPasswordResetResponse>> RequestPasswordReset(RequestPasswordResetCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult<ResetPasswordResponse>> ResetPassword(ResetPasswordCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }
}