using KasifApi.Models;
using KasifApi.DTO;

namespace KasifApi.Interfaces;

public interface INotification
{
    Task<List<NotificationDto>> GetUserNotificationsAsync(int userId);
    Task<NotificationDto> MarkAsReadAsync(int notificationId);
    Task SendNotificationAsync(int userId, string message);
}