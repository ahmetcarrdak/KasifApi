using KasifApi.Interfaces;
using KasifApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KasifApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotification _notificationService;

        public NotificationController(INotification notificationService)
        {
            _notificationService = notificationService;
        }

        // Kullanıcıya ait tüm bildirimleri al
        [HttpGet("GetNotifications/{userId}")]
        public async Task<IActionResult> GetUserNotifications(int userId)
        {
            var notifications = await _notificationService.GetUserNotificationsAsync(userId);
            return Ok(notifications);
        }

        // Bildirimi okundu olarak işaretle
        [HttpPost("markAsRead/{notificationId}")]
        public async Task<IActionResult> MarkAsRead(int notificationId)
        {
            var notification = await _notificationService.MarkAsReadAsync(notificationId);
            if (notification == null)
            {
                return NotFound("Notification not found.");
            }
            return Ok(notification);
        }

        // Bildirim gönder
        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] SendNotificationRequest request)
        {
            await _notificationService.SendNotificationAsync(request.UserId, request.Message);
            return Ok("Notification sent.");
        }
    }

    public class SendNotificationRequest
    {
        public int UserId { get; set; }
        public string Message { get; set; }
    }
}