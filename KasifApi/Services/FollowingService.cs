using KasifApi.DTO;
using KasifApi.Interfaces;
using KasifApi.Models;
using KasifApi.Data;
using Microsoft.EntityFrameworkCore;

namespace KasifApi.Services
{
    public class FollowingService : IFollowing
    {
        private readonly KasifDbContext _context;

        public FollowingService(KasifDbContext context)
        {
            _context = context;
        }

        // Takip etme işlemi
        public async Task<FollowingDto> CreateAsync(FollowingCreateDto followingCreateDto)
        {
            // Takip zaten var mı kontrol et
            var existingFollow = await _context.Followings
                .FirstOrDefaultAsync(f => f.From == followingCreateDto.From && f.To == followingCreateDto.To);

            if (existingFollow != null)
            {
                throw new Exception("Bu kullanıcıyı zaten takip ediyorsunuz.");
            }

            // Yeni takip oluştur
            var newFollow = new Following
            {
                From = followingCreateDto.From,
                To = followingCreateDto.To
            };

            await _context.Followings.AddAsync(newFollow);
            await _context.SaveChangesAsync();

            return new FollowingDto
            {
                Id = newFollow.FollowingId,
                From = newFollow.From,
                To = newFollow.To,
                Created = DateTime.UtcNow,
                FromUser = new Customer { CustomerId = newFollow.From, Name = "From User" }, // İlgili kullanıcı bilgilerini buraya ekleyin
                ToUser = new Customer { CustomerId = newFollow.To, Name = "To User" } // Aynı şekilde buraya da ekleyin
            };
        }

        // Tüm takipleri listele
        public async Task<List<FollowingDto>> GetAllAsync()
        {
            return await _context.Followings
                .Select(f => new FollowingDto
                {
                    Id = f.FollowingId,
                    From = f.From,
                    To = f.To,
                    FromUser = new Customer {CustomerId = f.From, Name = "From User" },  // İlgili kullanıcı bilgilerini buraya ekleyin
                    ToUser = new Customer { CustomerId = f.To, Name = "To User" } // Aynı şekilde buraya da ekleyin
                })
                .ToListAsync();
        }

        // Takip olup olmadığını kontrol et
        public async Task<bool> IsFollowingAsync(int fromUserId, int toUserId)
        {
            return await _context.Followings
                .AnyAsync(f => f.From == fromUserId && f.To == toUserId);
        }
        
        // fromUserId, toUserId tarafından takip ediliyor mu?
        public async Task<bool> IsBeingFollowedByAsync(int fromUserId, int toUserId)
        {
            return await _context.Followings
                .AnyAsync(f => f.From == toUserId && f.To == fromUserId);
        }

        // Takip edilen kullanıcıyı kimlerin takip ettiğini döndür
        public async Task<List<Customer>> IsBeingFollowedAsync(int toUserId)
        {
            var followers = await _context.Followings
                .Where(f => f.To == toUserId) // Takip edilen kullanıcı ID'sine göre filtreleme
                .Select(f => f.From) // Takipçi ID'lerini al
                .ToListAsync();

            // Takipçi kullanıcı bilgilerini almak
            var followerDetails = await _context.Customers
                .Where(c => followers.Contains(c.CustomerId)) // Takipçi ID'lerine göre filtreleme
                .Select(c => new Customer
                {
                    CustomerId = c.CustomerId,
                    Name = c.Name
                })
                .ToListAsync();

            return followerDetails;
        }

        // Takipten çıkma işlemi
        public async Task<bool> UnfollowAsync(int fromUserId, int toUserId)
        {
            var follow = await _context.Followings
                .FirstOrDefaultAsync(f => f.From == fromUserId && f.To == toUserId);

            if (follow == null)
            {
                return false;
            }

            _context.Followings.Remove(follow);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
