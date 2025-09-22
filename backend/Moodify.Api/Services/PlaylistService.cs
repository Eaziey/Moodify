using Moodify.Api.Models;
using Moodify.Api.Services.IServices;
using Moodify.Api.Repositories.Interfaces;

namespace Moodify.Api.Services{

    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _repository;

        public PlaylistService(IPlaylistRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Playlist?>> GetUserPlaylistsAsync(Guid userId)
        {

            if (userId.Equals(Guid.Empty))
            {
                return [];
            }

            var userPlaylists = await _repository.GetUserPlaylistsAsync(userId);

            return userPlaylists;
        }

        public async Task<Playlist?> GetPlaylistByIdAsync(Guid playlistId)
        {
            if (playlistId.Equals(Guid.Empty))
            {
                return null;
            }

            var playlist = await _repository.GetByIdAsync(playlistId);

            return playlist;
        }
    }
}