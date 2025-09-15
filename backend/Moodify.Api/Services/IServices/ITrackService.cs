using Moodify.Api.Models;

namespace Moodify.Api.Services.IServices
{
    public interface ITrackService
    {
        Task<IEnumerable<Track>> GetAllTracksAsync();
        Task<Track?> GetTrackByIdAsync(Guid id);
    }
} 