using KasifApi.Models;
using KasifApi.DTO;

namespace KasifApi.Interfaces;

public interface IPostSaved
{
    Task<PostSavedDto> SavePostAsync(PostSavedCreateDto postSavedCreateDto);
    Task<string> RemoveSavedPostAsync(PostSavedDeleteDto postSavedDeleteDto);
    Task<bool> IsPostSavedByUserAsync(int customerId, int postId);
    Task<List<PostSavedDto>> GetSavedPostsByUserAsync(int customerId);
}