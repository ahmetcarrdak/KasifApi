using KasifApi.Models;
using KasifApi.DTO;

namespace KasifApi.Interfaces;

public interface IPost
{
    Task<PostDto> CreatePostAsync(PostCreateDto postCreateDto);
    Task<PostDto> GetPostByIdAsync(int postId);
    Task<List<PostDto>> GetAllPostsAsync();
    Task<string> UpdatePostAsync(PostUpdateDto postUpdateDto);
    Task<string> ToggleDeleteAsync(int postId);
    Task<string> ToggleActivateAsync(int postId);
}