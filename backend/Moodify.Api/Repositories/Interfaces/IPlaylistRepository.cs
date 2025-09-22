using Moodify.Api.Models;

namespace Moodify.Api.Repositories.Interfaces{

    public interface IPlaylistRepository
    {

        Task<IEnumerable<Playlist?>> GetUserPlaylistsAsync(Guid userId);

        Task<Playlist?> GetByIdAsync(Guid playlistId);

        Task<bool> SaveChangesAsync();

    }
}