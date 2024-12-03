using KasifApi.Interfaces;
using KasifApi.DTO;
using KasifApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace KasifApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessage _messageService;

        public MessagesController(IMessage message)
        {
            _messageService = message;
        }

        // Yeni mesaj gönderme
        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] MessageDto messageDto)
        {
            var message = await _messageService.SendMessageAsync(messageDto);
            return Ok(message);
        }

        // Mesaj okundu olarak işaretleme
        [HttpPut("mark-as-read/{messageId}")]
        public async Task<IActionResult> MarkAsRead(int messageId)
        {
            var message = await _messageService.MarkAsReadAsync(messageId);
            if (message == null)
            {
                return NotFound("Mesaj bulunamadı.");
            }
            return Ok(message);
        }

        // Kullanıcıya ait mesajları listeleme
        [HttpGet("inbox/{userId}")]
        public async Task<IActionResult> GetMessages(int userId)
        {
            var messages = await _messageService.GetMessagesAsync(userId);
            return Ok(messages);
        }

        // İki kullanıcı arasındaki mesajları getirme
        [HttpGet("conversation/{fromUserId}/{toUserId}")]
        public async Task<IActionResult> GetMessagesBetweenUsers(int fromUserId, int toUserId)
        {
            var messages = await _messageService.GetMessagesBetweenUsersAsync(fromUserId, toUserId);
            return Ok(messages);
        }
    }
}