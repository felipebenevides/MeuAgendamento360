using System;

namespace myschedule360.Domain.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; } // info, success, warning, error
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public string UserId { get; set; } // Recipient user ID
        public string? Link { get; set; } // Optional link to navigate to

        public Notification()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            IsRead = false;
            Type = "info";
        }

        public Notification(string title, string message, string userId, string type = "info", string link = null)
        {
            Id = Guid.NewGuid();
            Title = title;
            Message = message;
            Type = type;
            UserId = userId;
            Link = link;
            CreatedAt = DateTime.UtcNow;
            IsRead = false;
        }

        public void MarkAsRead()
        {
            IsRead = true;
        }
    }
}