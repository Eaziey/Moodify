using Moodify.Api.Models;

namespace Moodify.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        
        Task<User?> GetBySpotifyIdAsync(string SpotifyId);
        Task AddAsync(User user);
        Task<bool> UpdateSpotifyDetailsAsync(Guid Id, string? SpotifyId = null, string? SpotifyEmail = null, string? SpotifyDisplayName = null, DateTime? SpotifyLinkedAt = null);
        Task<bool> SaveChangesAsync();
    }
} 