using Moodify.Api.Models;
using Moodify.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moodify.Api.Data;

namespace Moodify.Api.Repositories
{
    public class TrackRepository: ITrackRepository
    {

        private readonly AppDbContext _context;

        public TrackRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Track>> GetAllAsync() {
            return await _context.Tracks.ToListAsync();
        }
        public async Task<Track?> GetByIdAsync(Guid id)
        {
            var task = await _context.Tracks.FirstOrDefaultAsync(track => track.Id == id);

            return task;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}