using Moodify.Api.Dtos;

namespace Moodify.Api.Dtos
{
    public class UserLoginReturnDto
    {
        public UserDto? User { get; set; } = null;
       

        public string Token { get; set; } = null!;

    }
}