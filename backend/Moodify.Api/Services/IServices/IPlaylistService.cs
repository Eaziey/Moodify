using Moodify.Api.Models;

namespace Moodify.Api.Services.IServices{

    public interface IPlaylistService
    {

        Task<IEnumerable<Playlist?>> GetUserPlaylistsAsync(Guid userId);

        Task<Playlist?> GetPlaylistByIdAsync(Guid playlistId);
    }
}