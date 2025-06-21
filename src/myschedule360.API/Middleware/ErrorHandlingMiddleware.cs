using System.Net;
using System.Text.Json;

namespace myschedule360.API.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = exception switch
        {
            InvalidOperationException => new { message = exception.Message, statusCode = (int)HttpStatusCode.BadRequest },
            UnauthorizedAccessException => new { message = "Unauthorized", statusCode = (int)HttpStatusCode.Unauthorized },
            _ => new { message = "Internal server error", statusCode = (int)HttpStatusCode.InternalServerError }
        };

        context.Response.StatusCode = response.statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}