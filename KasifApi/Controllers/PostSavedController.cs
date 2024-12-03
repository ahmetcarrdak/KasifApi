using KasifApi.Interfaces;
using KasifApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KasifApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostSavedController : ControllerBase
    {
        private readonly IPostSaved _postSavedService;

        public PostSavedController(IPostSaved postSavedService)
        {
            _postSavedService = postSavedService;
        }

        // Post kaydetme
        [HttpPost("save")]
        public async Task<ActionResult<PostSavedDto>> SavePost([FromBody] PostSavedCreateDto postSavedCreateDto)
        {
            var postSaved = await _postSavedService.SavePostAsync(postSavedCreateDto);
            if (postSaved == null)
            {
                return BadRequest("Bu post zaten kaydedilmiş.");
            }

            return CreatedAtAction(nameof(GetSavedPostsByUser), new { customerId = postSavedCreateDto.CustomerId }, postSaved);
        }

        // Post kayıttan çıkarma
        [HttpPost("remove")]
        public async Task<ActionResult> RemoveSavedPost([FromBody] PostSavedDeleteDto postSavedDeleteDto)
        {
            var result = await _postSavedService.RemoveSavedPostAsync(postSavedDeleteDto);
            if (result == "Bu post daha önce kaydedilmemiş.")
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        // Kullanıcının kaydettiği tüm postları getirme
        [HttpGet("saved/{customerId}")]
        public async Task<ActionResult<List<PostSavedDto>>> GetSavedPostsByUser(int customerId)
        {
            var savedPosts = await _postSavedService.GetSavedPostsByUserAsync(customerId);
            return Ok(savedPosts);
        }

        // Kullanıcı bir postu kaydetmiş mi kontrol etme
        [HttpGet("is-saved/{customerId}/{postId}")]
        public async Task<ActionResult<bool>> IsPostSaved(int customerId, int postId)
        {
            var isSaved = await _postSavedService.IsPostSavedByUserAsync(customerId, postId);
            return Ok(isSaved);
        }
    }
}
