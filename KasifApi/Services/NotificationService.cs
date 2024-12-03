using KasifApi.Data;
using KasifApi.Models;
using KasifApi.DTO;
using KasifApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KasifApi.Services
{
    public class NotificationService : INotification
    {
        private readonly KasifDbContext _context;

        public NotificationService(KasifDbContext context)
        {
            _context = context;
        }

        // Kullanıcıya ait bildirimleri al
        public async Task<List<NotificationDto>> GetUserNotificationsAsync(int userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return notifications.Select(n => new NotificationDto
            {
                Id = n.Id,
                Message = n.Message,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt
            }).ToList();
        }

        // Bildirimi okundu olarak işaretle
        public async Task<NotificationDto> MarkAsReadAsync(int notificationId)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(n => n.Id == notificationId);
            if (notification == null)
                return null;

            notification.IsRead = true;
            notification.ReadAt = DateTime.Now;

            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();

            return new NotificationDto
            {
                Id = notification.Id,
                Message = notification.Message,
                IsRead = notification.IsRead,
                CreatedAt = notification.CreatedAt
            };
        }

        // Bildirim gönder
        public async Task SendNotificationAsync(int userId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Message = message,
                IsRead = false,
                CreatedAt = DateTime.Now
            };

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }
    }
}
