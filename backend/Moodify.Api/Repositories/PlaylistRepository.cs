using Moodify.Api.Models;
using Moodify.Api.Data;
using Moodify.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Moodify.Api.Repositories{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly AppDbContext _context;

        public PlaylistRepository(AppDbContext context) {
            _context = context;
        }
        public async Task<IEnumerable<Playlist?>> GetUserPlaylistsAsync(Guid userId)
        {
            return await _context.Playlists
                .Where(p => p.UserId == userId)
                .Include(p => p.PlaylistTracks)
                    .ThenInclude(pt => pt.Track).ToListAsync();
        }

        public async Task<Playlist?> GetByIdAsync(Guid playlistId)
        {
            return await _context.Playlists.Include(p => p.PlaylistTracks)
                .ThenInclude(pt => pt.Track)
                    .FirstOrDefaultAsync(playlist => playlist.Id == playlistId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}