using KasifApi.DTO;
using KasifApi.Models;

namespace KasifApi.Interfaces
{
    public interface IMessage
    {
        // Yeni mesaj gönderme
        Task<Message> SendMessageAsync(MessageDto messageDto);

        // Mesaj okuma işlemi
        Task<Message> MarkAsReadAsync(int messageId);

        // Kullanıcının aldığı mesajları listeleme
        Task<IEnumerable<MessageResponseDto>> GetMessagesAsync(int userId);

        // Kullanıcılar arasındaki mesajları al
        Task<IEnumerable<MessageResponseDto>> GetMessagesBetweenUsersAsync(int fromUserId, int toUserId);
    }
}