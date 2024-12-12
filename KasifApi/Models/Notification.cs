namespace KasifApi.Models;

public class Notification
{
    public int NotificationId { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }  // Okundu mu?
    public DateTime CreatedAt { get; set; }
    public DateTime? ReadAt { get; set; }

    public Customer User { get; set; }  // Bildirimi alan kullanıcı
}