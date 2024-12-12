using KasifApi.Interfaces;
using KasifApi.DTO;
using KasifApi.Models;
using KasifApi.Data;
using Microsoft.EntityFrameworkCore;

namespace KasifApi.Services
{
    public class PostService : IPost
    {
        private readonly KasifDbContext _context;

        public PostService(KasifDbContext context)
        {
            _context = context;
        }

        public async Task<PostDto> CreatePostAsync(PostCreateDto postCreateDto)
        {
            var post = new Post
            {
                CustomerId = postCreateDto.CustomerId,
                GalleryId = postCreateDto.GalleryId,
                AddressId = postCreateDto.AddressesId,
                Description = postCreateDto.Description,
                IsActived = true,
                IsDeleted = false
            };

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return new PostDto
            {
                Id = post.PostId,
                CustomerId = post.CustomerId,
                GalleryId = post.GalleryId,
                AddressesId = post.AddressId,
                Description = post.Description,
                IsActived = post.IsActived,
                IsDeleted = post.IsDeleted
            };
        }

        public async Task<PostDto> GetPostByIdAsync(int postId)
        {
            var post = await _context.Posts
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(p => p.PostId == postId);

            if (post == null)
            {
                return null;
            }

            return new PostDto
            {
                Id = post.PostId,
                CustomerId = post.CustomerId,
                GalleryId = post.GalleryId,
                AddressesId = post.AddressId,
                Description = post.Description,
                IsActived = post.IsActived,
                IsDeleted = post.IsDeleted
            };
        }

        public async Task<List<PostDto>> GetAllPostsAsync()
        {
            var posts = await _context.Posts
                .Include(p => p.Customer)
                .ToListAsync();

            return posts.Select(post => new PostDto
            {
                Id = post.PostId,
                CustomerId = post.CustomerId,
                GalleryId = post.GalleryId,
                AddressesId = post.AddressId,
                Description = post.Description,
                IsActived = post.IsActived,
                IsDeleted = post.IsDeleted
            }).ToList();
        }

        public async Task<string> UpdatePostAsync(PostUpdateDto postUpdateDto)
        {
            var post = await _context.Posts
                .FirstOrDefaultAsync(p => p.PostId == postUpdateDto.Id);

            if (post == null)
            {
                return "Post bulunamadı.";
            }

            post.Description = postUpdateDto.Description;
            post.IsActived = postUpdateDto.IsActived;
            post.IsDeleted = postUpdateDto.IsDeleted;

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();

            return "Post başarıyla güncellendi.";
        }

        public async Task<string> ToggleDeleteAsync(int postId)
        {
            var post = await _context.Posts
                .FirstOrDefaultAsync(p => p.PostId == postId);

            if (post == null)
            {
                return "Post bulunamadı.";
            }

            post.IsDeleted = !post.IsDeleted;
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();

            return post.IsDeleted ? "Post silindi." : "Post silinmesi kaldırıldı.";
        }

        public async Task<string> ToggleActivateAsync(int postId)
        {
            var post = await _context.Posts
                .FirstOrDefaultAsync(p => p.PostId == postId);

            if (post == null)
            {
                return "Post bulunamadı.";
            }

            post.IsActived = !post.IsActived;
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();

            return post.IsActived ? "Post aktif oldu." : "Post pasif oldu.";
        }
    }
}
