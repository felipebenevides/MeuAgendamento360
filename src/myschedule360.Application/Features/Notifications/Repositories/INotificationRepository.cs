using myschedule360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myschedule360.Application.Features.Notifications.Repositories
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetUserNotificationsAsync(string userId, bool unreadOnly = false, int limit = 50);
        Task<Notification> GetByIdAsync(Guid id);
        Task<Notification> CreateAsync(Notification notification);
        Task<bool> MarkAsReadAsync(Guid id);
        Task<bool> MarkAllAsReadAsync(string userId);
        Task<bool> DeleteAsync(Guid id);
        Task<int> GetUnreadCountAsync(string userId);
    }
}