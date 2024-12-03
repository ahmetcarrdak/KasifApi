using KasifApi.Interfaces;
using KasifApi.DTO;
using KasifApi.Models;
using KasifApi.Data;
using Microsoft.EntityFrameworkCore;

namespace KasifApi.Services
{
    public class PostSavedService : IPostSaved
    {
        private readonly KasifDbContext _context;

        public PostSavedService(KasifDbContext context)
        {
            _context = context;
        }

        // Post'u kaydetme
        public async Task<PostSavedDto> SavePostAsync(PostSavedCreateDto postSavedCreateDto)
        {
            var existingSavedPost = await _context.PostSaveds
                .FirstOrDefaultAsync(ps => ps.CustomerId == postSavedCreateDto.CustomerId && ps.PostId == postSavedCreateDto.PostId);

            if (existingSavedPost != null)
            {
                return null; // Post zaten kaydedilmiş
            }

            var postSaved = new PostSaved
            {
                CustomerId = postSavedCreateDto.CustomerId,
                PostId = postSavedCreateDto.PostId
            };

            await _context.PostSaveds.AddAsync(postSaved);
            await _context.SaveChangesAsync();

            return new PostSavedDto
            {
                Id = postSaved.Id,
                CustomerId = postSaved.CustomerId,
                PostId = postSaved.PostId
            };
        }

        // Post'u kayıttan çıkarma
        public async Task<string> RemoveSavedPostAsync(PostSavedDeleteDto postSavedDeleteDto)
        {
            var postSaved = await _context.PostSaveds
                .FirstOrDefaultAsync(ps => ps.CustomerId == postSavedDeleteDto.CustomerId && ps.PostId == postSavedDeleteDto.PostId);

            if (postSaved == null)
            {
                return "Bu post daha önce kaydedilmemiş.";
            }

            _context.PostSaveds.Remove(postSaved);
            await _context.SaveChangesAsync();

            return "Post kayıttan çıkarıldı.";
        }

        // Kullanıcı bir postu kaydetmiş mi kontrol etme
        public async Task<bool> IsPostSavedByUserAsync(int customerId, int postId)
        {
            var postSaved = await _context.PostSaveds
                .FirstOrDefaultAsync(ps => ps.CustomerId == customerId && ps.PostId == postId);

            return postSaved != null;
        }

        // Kullanıcının kaydettiği tüm postları getirme
        public async Task<List<PostSavedDto>> GetSavedPostsByUserAsync(int customerId)
        {
            var savedPosts = await _context.PostSaveds
                .Where(ps => ps.CustomerId == customerId)
                .ToListAsync();

            return savedPosts.Select(postSaved => new PostSavedDto
            {
                Id = postSaved.Id,
                CustomerId = postSaved.CustomerId,
                PostId = postSaved.PostId
            }).ToList();
        }
    }
}
