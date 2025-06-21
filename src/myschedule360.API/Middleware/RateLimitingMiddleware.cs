using System.Collections.Concurrent;

namespace myschedule360.API.Middleware;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ConcurrentDictionary<string, (DateTime LastRequest, int RequestCount)> _clients = new();
    private readonly int _maxRequests = 100;
    private readonly TimeSpan _timeWindow = TimeSpan.FromMinutes(1);

    public RateLimitingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var clientId = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var now = DateTime.UtcNow;

        if (_clients.TryGetValue(clientId, out var clientInfo))
        {
            if (now - clientInfo.LastRequest < _timeWindow)
            {
                if (clientInfo.RequestCount >= _maxRequests)
                {
                    context.Response.StatusCode = 429;
                    await context.Response.WriteAsync("Rate limit exceeded");
                    return;
                }
                _clients[clientId] = (now, clientInfo.RequestCount + 1);
            }
            else
            {
                _clients[clientId] = (now, 1);
            }
        }
        else
        {
            _clients[clientId] = (now, 1);
        }

        await _next(context);
    }
}