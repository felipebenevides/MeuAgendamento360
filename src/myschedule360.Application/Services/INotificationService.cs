using myschedule360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myschedule360.Application.Services
{
    public interface INotificationService
    {
        Task SendToUserAsync(string userId, string title, string message, string type = "info");
        Task SendToAllAsync(string title, string message, string type = "info");
        Task SendToGroupAsync(string groupName, string title, string message, string type = "info");
        Task<List<Notification>> GetUserNotificationsAsync(string userId, bool unreadOnly = false, int limit = 50);
        Task<bool> MarkAsReadAsync(Guid notificationId);
        Task<bool> MarkAllAsReadAsync(string userId);
        Task<int> GetUnreadCountAsync(string userId);
    }
}