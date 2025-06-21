using MediatR;
using Microsoft.AspNetCore.Mvc;
using myschedule360.Application.Features.Public.Queries;

namespace myschedule360.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PublicController : ControllerBase
{
    private readonly IMediator _mediator;

    public PublicController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("business/{slug}")]
    public async Task<ActionResult<GetBusinessBySlugResponse>> GetBusinessBySlug(string slug)
    {
        try
        {
            var result = await _mediator.Send(new GetBusinessBySlugQuery(slug));
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}