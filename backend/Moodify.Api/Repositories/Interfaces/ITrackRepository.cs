using Moodify.Api.Models;

namespace Moodify.Api.Repositories.Interfaces
{
    public interface ITrackRepository
    {
        Task<IEnumerable<Track>> GetAllTracksAsync();
        Task<Track> GetTrackByIdAsync(Guid id);
        Task<bool> SaveChangesAsync();
    }
} 