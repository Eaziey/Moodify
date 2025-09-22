using Moodify.Api.Models;

namespace Moodify.Api.Repositories.Interfaces
{
    public interface ITrackRepository
    {
        Task<IEnumerable<Track>> GetAllAsync();
        Task<Track?> GetByIdAsync(Guid id);
        Task<bool> SaveChangesAsync();
    }
} 