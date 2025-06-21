using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myschedule360.Application.Services;
using System;
using System.Threading.Tasks;

namespace myschedule360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetNotifications([FromQuery] bool unreadOnly = false, [FromQuery] int limit = 50)
        {
            var userId = User.Identity.Name;
            var notifications = await _notificationService.GetUserNotificationsAsync(userId, unreadOnly, limit);
            return Ok(notifications);
        }

        [HttpGet("unread-count")]
        [Authorize]
        public async Task<IActionResult> GetUnreadCount()
        {
            var userId = User.Identity.Name;
            var count = await _notificationService.GetUnreadCountAsync(userId);
            return Ok(new { count });
        }

        [HttpPost("{id}/read")]
        [Authorize]
        public async Task<IActionResult> MarkAsRead(Guid id)
        {
            var result = await _notificationService.MarkAsReadAsync(id);
            if (!result)
                return NotFound();
            return Ok();
        }

        [HttpPost("mark-all-read")]
        [Authorize]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var userId = User.Identity.Name;
            var result = await _notificationService.MarkAllAsReadAsync(userId);
            return Ok(new { success = result });
        }

        [HttpPost("send-to-user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SendToUser([FromBody] NotificationRequest request)
        {
            await _notificationService.SendToUserAsync(request.UserId, request.Title, request.Message, request.Type);
            return Ok(new { message = "Notification sent successfully" });
        }

        [HttpPost("send-to-all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SendToAll([FromBody] BroadcastNotificationRequest request)
        {
            await _notificationService.SendToAllAsync(request.Title, request.Message, request.Type);
            return Ok(new { message = "Broadcast notification sent successfully" });
        }

        [HttpPost("send-to-group")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SendToGroup([FromBody] GroupNotificationRequest request)
        {
            await _notificationService.SendToGroupAsync(request.GroupName, request.Title, request.Message, request.Type);
            return Ok(new { message = "Group notification sent successfully" });
        }

        [HttpPost("test")]
        [AllowAnonymous]
        public async Task<IActionResult> SendTestNotification([FromBody] TestNotificationRequest request)
        {
            // This endpoint is for testing purposes only
            await _notificationService.SendToAllAsync(
                request.Title ?? "Test Notification",
                request.Message ?? "This is a test notification from the API",
                request.Type ?? "info");
            
            return Ok(new { message = "Test notification sent successfully" });
        }
    }

    public class NotificationRequest
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; } = "info";
    }

    public class BroadcastNotificationRequest
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; } = "info";
    }

    public class GroupNotificationRequest
    {
        public string GroupName { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; } = "info";
    }

    public class TestNotificationRequest
    {
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? Type { get; set; }
    }
}