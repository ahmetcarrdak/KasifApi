using KasifApi.DTO;
using KasifApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KasifApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowingController : ControllerBase
    {
        private readonly IFollowing _followingService;

        public FollowingController(IFollowing followingService)
        {
            _followingService = followingService;
        }

        // Takip etme işlemi
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] FollowingCreateDto followingCreateDto)
        {
            try
            {
                var result = await _followingService.CreateAsync(followingCreateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Tüm takipleri listele
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _followingService.GetAllAsync();
            return Ok(result);
        }

        // Kullanıcıyı takip edip etmediğini kontrol etme
        [HttpGet("is-following/{fromUserId}/{toUserId}")]
        public async Task<IActionResult> IsFollowing(int fromUserId, int toUserId)
        {
            var result = await _followingService.IsFollowingAsync(fromUserId, toUserId);
            return Ok(new { IsFollowing = result });
        }
        
        // Kullanıcı ziyaret ettiği kullanıcı tarafından takip ediliyor mu
        [HttpGet("is-being-followed-by/{fromUserId}/{toUserId}")]
        public async Task<IActionResult> IsBeingFollowedBy(int fromUserId, int toUserId)
        {
            var result = await _followingService.IsBeingFollowedByAsync(fromUserId, toUserId);
            return Ok(new { IsBeingFollowedBy = result });
        }

        
        // Takip edilen kullanıcıyı kimlerin takip ettiğini kontrol etme
        [HttpGet("being-followed/{toUserId}")]
        public async Task<IActionResult> IsBeingFollowed(int toUserId)
        {
            var result = await _followingService.IsBeingFollowedAsync(toUserId);
            return Ok(result);
        }

        // Takipten çıkma işlemi
        [HttpDelete("UnFollow/{fromUserId}/{toUserId}")]
        public async Task<IActionResult> Unfollow(int fromUserId, int toUserId)
        {
            var result = await _followingService.UnfollowAsync(fromUserId, toUserId);
            if (result)
            {
                return Ok("Takipten çıkıldı.");
            }
            return NotFound("Takip bulunamadı.");
        }
    }
}