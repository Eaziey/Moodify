using Microsoft.AspNetCore.Mvc;
using Moodify.Api.Models;
using Moodify.Api.Services.IServices;
using Moodify.Api.Dtos;
using System.Security;


namespace Moodify.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("logIn")]
        public async Task<IActionResult> LogInUser([FromBody] UserLoginDto user)
        {

            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                var _user = await _authService.VarifyUserLoginDetailsAsync(user);

                return StatusCode(201, _user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(401, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignInUser([FromBody] UserRegistrationDto user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                var _user = await _authService.AddUserAsync(user);

                return Ok(_user);
            }
            catch (VerificationException)
            {
                return StatusCode(401, new { message = "Login failed. Please make sure your email and password are correct." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }

    }
}
