using Moodify.Api.Models;
using Moodify.Api.Data;
using Moodify.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Moodify.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User?> GetBySpotifyIdAsync(string SpotifyId)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.SpotifyId == SpotifyId);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateSpotifyDetailsAsync(Guid Id, string? SpotifyId = null, string? SpotifyEmail = null, string? SpotifyDisplayName = null, DateTime? SpotifyLinkedAt = null )
        {
            var user = await GetByIdAsync(Id);

            if (user == null)
            {
                return false;
            }

            user.SpotifyId = SpotifyId ?? user.SpotifyId;
            user.SpotifyEmail = SpotifyEmail ?? user.SpotifyEmail;
            user.SpotifyDisplayName = SpotifyDisplayName ?? user.SpotifyDisplayName;
            user.LastModifiedAt = DateTime.UtcNow;
            
            if (SpotifyId != null || SpotifyEmail != null || SpotifyDisplayName != null || SpotifyLinkedAt != null)
            {
                user.SpotifyLinkedAt = SpotifyLinkedAt;
            }


            return await _context.SaveChangesAsync() > 0;
        }
    } 
}