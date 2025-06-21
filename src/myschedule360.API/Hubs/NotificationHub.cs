using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace myschedule360.API.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string userId, string title, string message, string type)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", new
            {
                id = System.Guid.NewGuid().ToString(),
                title,
                message,
                type,
                timestamp = System.DateTime.UtcNow,
                read = false
            });
        }

        public async Task SendBroadcastNotification(string title, string message, string type)
        {
            await Clients.All.SendAsync("ReceiveNotification", new
            {
                id = System.Guid.NewGuid().ToString(),
                title,
                message,
                type,
                timestamp = System.DateTime.UtcNow,
                read = false
            });
        }

        public async Task SendGroupNotification(string groupName, string title, string message, string type)
        {
            await Clients.Group(groupName).SendAsync("ReceiveNotification", new
            {
                id = System.Guid.NewGuid().ToString(),
                title,
                message,
                type,
                timestamp = System.DateTime.UtcNow,
                read = false
            });
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}