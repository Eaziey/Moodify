using Moodify.Api.Models;
using Moodify.Api.Repositories;
using Moodify.Api.Services.IServices;

namespace Moodify.Api.Services
{
    public class UserService : IUserService
    {

        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
        {
            _repository = repository;   
        }
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                return null;
            }

            var user = await _repository.GetByIdAsync(id);

            return user;
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            if (email.Trim() == "")
            {
                return null;
            }

            var user = await _repository.GetByEmailAsync(email);

            return user;
        }
        public async Task<bool> AddUserAsync(User user)
        {

            var existingUser = await _repository.GetByEmailAsync(user.Email);

            if (user == null || existingUser != null)
            {
                return false;
            }

            await _repository.AddAsync(user);

            var result = await _repository.SaveChangesAsync();

            return result;
        }
    
    }
}