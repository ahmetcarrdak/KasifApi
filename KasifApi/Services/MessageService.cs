using KasifApi.Interfaces;
using KasifApi.DTO;
using KasifApi.Models;
using KasifApi.Data;
using Microsoft.EntityFrameworkCore;

namespace KasifApi.Services
{
    public class MessageService : IMessage
    {
        private readonly KasifDbContext _context;

        public MessageService(KasifDbContext context)
        {
            _context = context;
        }

        // Yeni mesaj gönderme
        public async Task<Message> SendMessageAsync(MessageDto messageDto)
        {
            // Yeni mesaj oluşturuluyor
            var message = new Message
            {
                From = messageDto.From,
                To = messageDto.To,
                Type = messageDto.Type,
                Content = messageDto.Content,
                PostId = messageDto.PostId,
                IsRead = false, // Yeni gönderilen mesaj okunmamış olarak işaretlenir
                Created = DateTime.UtcNow
            };

            // Mesajı veritabanına ekle
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return message;
        }

        // Mesajı okundu olarak işaretle
        public async Task<Message> MarkAsReadAsync(int messageId)
        {
            var message = await _context.Messages.Include(message => message.FromUser).FirstOrDefaultAsync(m => m.Id == messageId);

            if (message == null)
            {
                return null;
            }

            message.IsRead = true;

            // Okunma tarihini de kaydedebiliriz (isteğe bağlı)
            message.ReadBy = message.FromUser;

            _context.Messages.Update(message);
            await _context.SaveChangesAsync();

            return message;
        }

        // Kullanıcının aldığı mesajları listele
        public async Task<IEnumerable<MessageResponseDto>> GetMessagesAsync(int userId)
        {
            return await _context.Messages
                .Where(m => m.To == userId)
                .Select(m => new MessageResponseDto
                {
                    Id = m.Id,
                    From = m.From,
                    To = m.To,
                    Type = m.Type,
                    Content = m.Content,
                    PostId = m.PostId,
                    IsRead = m.IsRead,
                    Created = m.Created
                })
                .ToListAsync();
        }

        // İki kullanıcı arasındaki mesajları getir
        public async Task<IEnumerable<MessageResponseDto>> GetMessagesBetweenUsersAsync(int fromUserId, int toUserId)
        {
            return await _context.Messages
                .Where(m => (m.From == fromUserId && m.To == toUserId) || (m.From == toUserId && m.To == fromUserId))
                .Select(m => new MessageResponseDto
                {
                    Id = m.Id,
                    From = m.From,
                    To = m.To,
                    Type = m.Type,
                    Content = m.Content,
                    PostId = m.PostId,
                    IsRead = m.IsRead,
                    Created = m.Created
                })
                .ToListAsync();
        }
    }
}
