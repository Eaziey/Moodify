using Moodify.Api.Dtos;
using Moodify.Api.Models;

namespace Moodify.Api.Services.IServices{

    public interface IAuthService
    {

        Task<UserDto?> AddUserAsync(UserRegistrationDto user);
        Task<UserDto?> VarifyUserLoginDetailsAsync(UserLoginDto user);
        User NormaliseUserDetails(User user);
        string SaltAndHashPassword(string password, out byte[] salt);

        bool VarifyPassword(string password, string hash, byte[] salt );
    
    }
}