using Moodify.Api.Models;

namespace Moodify.Api.Services.IServices
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserBySpotifyIdAsync(string spotifyId);
        Task<bool> AddUserAsync(User user);

        Task<User?> UpdateSpotifyDetailsAsync(Guid Id, string? SpotifyId = null, string? SpotifyEmail = null, string? SpotifyDisplayName = null);
    }
} 