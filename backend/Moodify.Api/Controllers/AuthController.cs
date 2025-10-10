using Microsoft.AspNetCore.Mvc;
using Moodify.Api.Models;
using Moodify.Api.Services.IServices;
using Moodify.Api.Dtos;
using System.Security;
using System.Text.Json;

namespace Moodify.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ISpotifyAuthService _spotifyAuthService;

        private readonly IUserService _userService;

        private readonly IWebHostEnvironment _env;

        public AuthController(IAuthService authService, ISpotifyAuthService spotifyAuthService, IWebHostEnvironment env, IUserService userService)
        {
            _authService = authService;
            _spotifyAuthService = spotifyAuthService;
            _env = env;
            _userService = userService;
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

                /*var _spotifyUser= await _authService.GetUserBySpotifyEmailAsync(user.SpotifyEmail);

                if (_spotifyUser == null)
                {
                    return NotFound(new { message = "User Not Found. User not integrated with spotify" });
                }*/

                if (user.UseSpotify)
                {
                    //set cookies and redirect to spotify

                    var state = Guid.NewGuid().ToString();

                    Response.Cookies.Append("flow_Type", "login", new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = !_env.IsDevelopment(),
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(10)
                    });

                    Response.Cookies.Append("spotify_state", state, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = !_env.IsDevelopment(),
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(10)
                    });

                    var redirectUrl = _spotifyAuthService.GetSpotifyUrl(state);
                    return Redirect(redirectUrl);
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
                    return BadRequest("User cannot be null.");
                }

                var _user = await _authService.AddUserAsync(user);

                if (user.Integrate == true && _user != null)
                {
                    var userId = _user.Id.ToString();
                    var state = Guid.NewGuid().ToString();

                    Response.Cookies.Append("user_id", userId, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = !_env.IsDevelopment(),
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(10)
                    });

                    Response.Cookies.Append("flow_type", "register", new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = !_env.IsDevelopment(),
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(10)
                    });

                    Response.Cookies.Append("spotify_state", state, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = !_env.IsDevelopment(),
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(10)
                    });

                    var redirectUrl = _spotifyAuthService.GetSpotifyUrl(state);
                    return Redirect(redirectUrl);

                }

                return Ok(_user);
            }
            catch (VerificationException ex)
            {
                return StatusCode(401, new { message = $"Sign in failed. {ex.Message}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpGet("spotify-login")]
        public IActionResult SportifyLogin()
        {

            var spotifyUrl = _spotifyAuthService.SpotifyLogin();

            return Redirect(spotifyUrl);
        }

        [HttpGet("spotify-callback")]
        public async Task<IActionResult> SpotifyCallback([FromQuery] string code, [FromBody] string state)
        {
            try
            {
                //var playlists = await _spotifyAuthService.SpotifyCallBack(code);
                //Console.WriteLine(code);

                var storedState = Request.Cookies["spotify_state"];
                var flowType = Request.Cookies["flow_type"];
                
                if (string.IsNullOrEmpty(state) || state != storedState)
                {
                    return Unauthorized("Invalid state");
                }
                
                var result = await _spotifyAuthService.SpotifyCallBack(code);

                if (!result.Success)
                {
                    return BadRequest(new { message = result.Message });
                }

                if (result.SpotifyUserData == null)
                {
                    return BadRequest(new { message = "Unable to get spotify user data." });
                }

                if (flowType == "register")
                {
                    var userId = Request.Cookies["user_id"]?? "";

                    var Id = Guid.Parse(userId);

                    var user = await _authService.UpdateSpotifyDetailsAsync(Id, result.SpotifyUserData.SpotifyId, result.SpotifyUserData.Email, result.SpotifyUserData.DisplayName);

                    return Ok(user);
                }
                else if (flowType == "login")
                {
                    var user = await _authService.GetUserBySpotifyIdAsync(result.SpotifyUserData.SpotifyId ?? "");

                    if(user == null){
                        return StatusCode(401, new { message = $"Login failed. User not found" });
                    }

                    return Ok(user);

                }

                return BadRequest("Unknown flow Type" );


            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Spotify callback failed: " + ex.Message });
            }

        }

    }
}
