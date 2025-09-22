using Moodify.Api.Models;
using Moodify.Api.Data;
using Moodify.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Moodify.Api.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task VarifyAsync(User user)
        {
            throw new NotImplementedException();
        }
        
        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

    }
}