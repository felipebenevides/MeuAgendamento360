using Microsoft.EntityFrameworkCore;
using myschedule360.Application.Features.Notifications.Repositories;
using myschedule360.Domain.Entities;
using myschedule360.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myschedule360.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IApplicationDbContext _context;

        public NotificationRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Notification>> GetUserNotificationsAsync(string userId, bool unreadOnly = false, int limit = 50)
        {
            var query = _context.Notifications
                .Where(n => n.UserId == userId);

            if (unreadOnly)
            {
                query = query.Where(n => !n.IsRead);
            }

            return await query
                .OrderByDescending(n => n.CreatedAt)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Notification> GetByIdAsync(Guid id)
        {
            return await _context.Notifications.FindAsync(id);
        }

        public async Task<Notification> CreateAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<bool> MarkAsReadAsync(Guid id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
                return false;

            notification.MarkAsRead();
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkAllAsReadAsync(string userId)
        {
            var unreadNotifications = await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();

            if (!unreadNotifications.Any())
                return false;

            foreach (var notification in unreadNotifications)
            {
                notification.MarkAsRead();
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
                return false;

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetUnreadCountAsync(string userId)
        {
            return await _context.Notifications
                .CountAsync(n => n.UserId == userId && !n.IsRead);
        }
    }
}