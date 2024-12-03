using KasifApi.DTO;
using KasifApi.Models;

namespace KasifApi.Interfaces;

public interface IFollowing
{
    // Takip işlemi başlat
    Task<FollowingDto> CreateAsync(FollowingCreateDto followingCreateDto);

    // Kullanıcıların tüm takiplerini listele
    Task<List<FollowingDto>> GetAllAsync();

    // Kullanıcıyı takip ediyor mu kontrol et
    Task<bool> IsFollowingAsync(int fromUserId, int toUserId);

    // Kullanıcı ziyaret ettiği kullanıcı tarafından takip ediliyor mu
    Task<bool> IsBeingFollowedByAsync(int fromUserId, int toUserId);

    // Bir kullanıcıyı kimlerin takip ettiğini kontrol et
    Task<List<Customer>> IsBeingFollowedAsync(int toUserId);

    // Kullanıcıları takipten çıkar
    Task<bool> UnfollowAsync(int fromUserId, int toUserId);
}