using Moodify.Api.Models;
using Moodify.Api.Services.IServices;
using Moodify.Api.Repositories.Interfaces;
using System.Security.Cryptography;
using System.Text;
using Moodify.Api.Dtos;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security;


namespace Moodify.Api.Services
{
    public class AuthService : IAuthService
    {

        private const int keySize = 64;
        private const int iterations = 350000;
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;

        public AuthService(IAuthRepository authRepository, IUserRepository userRepository)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
        }

        public async Task<UserDto?> VarifyUserLoginDetailsAsync(UserLoginDto user)
        {
            var _user = await _userRepository.GetByEmailAsync(user.Email);

            if(_user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            var isVarified = VarifyPassword(user.Password, _user.Password, Convert.FromHexString(_user.Salt));

            if (!isVarified)
            {
                throw new UnauthorizedAccessException("Password does not match");
            }

            UserDto retUser = new()
            {
                Id = _user.Id,
                Username = _user.Username,
                Email = _user.Email
            };

            return retUser;

        }

        public async Task<UserDto?> AddUserAsync(UserRegistrationDto user) {

            var _user = await _userRepository.GetByEmailAsync(user.Email);

            if (_user != null)
            {
                throw new VerificationException("User with Email already exists.");
            }

            var hashedPw = SaltAndHashPassword(user.Password, out byte[] salt);

            User newUser = new()
            {
                Username = user.Username,
                NormalisedUsername = "",
                Email = user.Email,
                Password = hashedPw,
                Salt = Convert.ToHexString(salt)
            };

            User normalisedUser = NormaliseUserDetails(newUser);
            await _userRepository.AddAsync(normalisedUser);
            var ret = await _userRepository.SaveChangesAsync();
            

            if (!ret)
            {
                throw new Exception("Something went wrong. Could not save user in db.");
            }

            var retUser = await _userRepository.GetByEmailAsync(normalisedUser.Email);

            if (retUser == null)
            {
                throw new Exception("Something went wrong. Could not get User from db.");
            }

            UserDto _userDto = new()
            {
                Id = retUser.Id,
                Username = retUser.Username,
                Email = retUser.Email
            };
                      

            return _userDto;
        }

        public User NormaliseUserDetails(User user)
        {
            var normalisedEmail = user.Email.Trim().ToLowerInvariant();
            var normalisedUsername = user.Username.Trim().ToLowerInvariant();

            user.Email = normalisedEmail;
            user.NormalisedUsername = normalisedUsername;

            return user;
        }

        public string SaltAndHashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                _hashAlgorithm,
                keySize
            );

            return Convert.ToHexString(hash);
        }

        public bool VarifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                _hashAlgorithm,
                keySize
            );

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));

        }
    }
}