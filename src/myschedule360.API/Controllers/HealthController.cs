using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myschedule360.Domain.Interfaces;

namespace myschedule360.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HealthController : ControllerBase
{
    private readonly IApplicationDbContext _context;

    public HealthController(IApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> GetHealth()
    {
        try
        {
            await _context.Users.AnyAsync();
            return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
        }
        catch
        {
            return StatusCode(503, new { status = "unhealthy", timestamp = DateTime.UtcNow });
        }
    }

    [HttpGet("ready")]
    public ActionResult GetReady()
    {
        return Ok(new { status = "ready", timestamp = DateTime.UtcNow });
    }
}