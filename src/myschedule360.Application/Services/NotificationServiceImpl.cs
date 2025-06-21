using Microsoft.AspNetCore.SignalR;
using myschedule360.API.Hubs;
using myschedule360.Application.Features.Notifications.Repositories;
using myschedule360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myschedule360.Application.Services
{
    public class NotificationServiceImpl : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly INotificationRepository _notificationRepository;

        public NotificationServiceImpl(
            IHubContext<NotificationHub> hubContext,
            INotificationRepository notificationRepository)
        {
            _hubContext = hubContext;
            _notificationRepository = notificationRepository;
        }

        public async Task SendToUserAsync(string userId, string title, string message, string type = "info")
        {
            // Create notification object
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                Title = title,
                Message = message,
                Type = type,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            // Save to database
            await _notificationRepository.CreateAsync(notification);

            // Send via SignalR
            await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", new
            {
                id = notification.Id.ToString(),
                title = notification.Title,
                message = notification.Message,
                type = notification.Type,
                timestamp = notification.CreatedAt,
                read = notification.IsRead
            });
        }

        public async Task SendToAllAsync(string title, string message, string type = "info")
        {
            // For broadcast notifications, we don't save them individually for each user
            // Instead, we just send them via SignalR
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
            // For group notifications, we don't save them individually for each user
            // Instead, we just send them via SignalR
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

        public async Task<List<Notification>> GetUserNotificationsAsync(string userId, bool unreadOnly = false, int limit = 50)
        {
            return await _notificationRepository.GetUserNotificationsAsync(userId, unreadOnly, limit);
        }

        public async Task<bool> MarkAsReadAsync(Guid notificationId)
        {
            return await _notificationRepository.MarkAsReadAsync(notificationId);
        }

        public async Task<bool> MarkAllAsReadAsync(string userId)
        {
            return await _notificationRepository.MarkAllAsReadAsync(userId);
        }

        public async Task<int> GetUnreadCountAsync(string userId)
        {
            return await _notificationRepository.GetUnreadCountAsync(userId);
        }
    }
}