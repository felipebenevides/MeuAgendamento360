using Microsoft.AspNetCore.SignalR;
using myschedule360.API.Hubs;
using System;
using System.Threading.Tasks;

namespace myschedule360.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendToUserAsync(string userId, string title, string message, string type = "info")
        {
            await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", new
            {
                id = Guid.NewGuid().ToString(),
                title,
                message,
                type,
                timestamp = DateTime.UtcNow,
                read = false
            });
        }

        public async Task SendToAllAsync(string title, string message, string type = "info")
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", new
            {
                id = Guid.NewGuid().ToString(),
                title,
                message,
                type,
                timestamp = DateTime.UtcNow,
                read = false
            });
        }

        public async Task SendToGroupAsync(string groupName, string title, string message, string type = "info")
        {
            await _hubContext.Clients.Group(groupName).SendAsync("ReceiveNotification", new
            {
                id = Guid.NewGuid().ToString(),
                title,
                message,
                type,
                timestamp = DateTime.UtcNow,
                read = false
            });
        }
    }
}