using KasifApi.Interfaces;
using KasifApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KasifApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPost _postService;

        public PostController(IPost postService)
        {
            _postService = postService;
        }

        // Post oluşturma
        [HttpPost("Create")]
        public async Task<ActionResult<PostDto>> CreatePost([FromBody] PostCreateDto postCreateDto)
        {
            var post = await _postService.CreatePostAsync(postCreateDto);
            if (post == null)
            {
                return BadRequest("Post oluşturulamadı.");
            }

            return CreatedAtAction(nameof(GetPostById), new { postId = post.Id }, post);
        }

        // Post ID ile getirme
        [HttpGet("{postId}")]
        public async Task<ActionResult<PostDto>> GetPostById(int postId)
        {
            var post = await _postService.GetPostByIdAsync(postId);
            if (post == null)
            {
                return NotFound("Post bulunamadı.");
            }

            return Ok(post);
        }

        // Tüm Post'ları getirme
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<PostDto>>> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        // Post güncelleme
        [HttpPut("Update")]
        public async Task<ActionResult> UpdatePost([FromBody] PostUpdateDto postUpdateDto)
        {
            var result = await _postService.UpdatePostAsync(postUpdateDto);
            if (result == "Post bulunamadı.")
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        // Post silme veya silmeyi geri alma
        [HttpPost("toggle-delete/{postId}")]
        public async Task<ActionResult> ToggleDelete(int postId)
        {
            var result = await _postService.ToggleDeleteAsync(postId);
            return Ok(result);
        }

        // Post aktifleştirme/pasifleştirme
        [HttpPost("toggle-activate/{postId}")]
        public async Task<ActionResult> ToggleActivate(int postId)
        {
            var result = await _postService.ToggleActivateAsync(postId);
            return Ok(result);
        }
    }
}
