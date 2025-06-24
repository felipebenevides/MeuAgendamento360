using myschedule360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myschedule360.Application.Services
{
    public interface INotificationService
    {
        System.Threading.Tasks.Task SendToUserAsync(string userId, string title, string message, string type = "info");
        System.Threading.Tasks.Task SendToAllAsync(string title, string message, string type = "info");
        System.Threading.Tasks.Task SendToGroupAsync(string groupName, string title, string message, string type = "info");
        System.Threading.Tasks.Task<List<Notification>> GetUserNotificationsAsync(string userId, bool unreadOnly = false, int limit = 50);
        System.Threading.Tasks.Task<bool> MarkAsReadAsync(Guid notificationId);
        System.Threading.Tasks.Task<bool> MarkAllAsReadAsync(string userId);
        System.Threading.Tasks.Task<int> GetUnreadCountAsync(string userId);
    }
}