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

            if (user == null)
            {
                return BadRequest("User cannot be null.");
            }

            try
            {

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
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(10)
                    });

                    Response.Cookies.Append("spotify_state", state, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(10)
                    });

                    var redirectUrl = _spotifyAuthService.GetSpotifyUrl(state);
                    return Ok(new { redirectUrl });
                }

                var _user = await _authService.VarifyUserLoginDetailsAsync(user);

                return StatusCode(201, _user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An unexpected error occurred. {ex.Message}" });
            }
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignInUser([FromBody] UserRegistrationDto user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null.");
            }

            try
            {

                var _user = await _authService.AddUserAsync(user);

                if (user.Integrate == true && _user != null)
                {
                    var userId = _user.Id.ToString();
                    var state = Guid.NewGuid().ToString();
                    //Console.WriteLine(state);

                    Response.Cookies.Append("user_id", userId, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(10)
                    });

                    Response.Cookies.Append("flow_type", "register", new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(10)
                    });

                    Response.Cookies.Append("spotify_state", state, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(10)
                    });

                    var redirectUrl = _spotifyAuthService.GetSpotifyUrl(state);
                    return Ok(new { redirectUrl });

                }

                return Redirect("https://opulent-space-giggle-qj7pgx5r9rx3x9pj-5173.app.github.dev/home");
            }
            catch (VerificationException ex)
            {
                return Conflict(new { message = $"Sign in failed. {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An unexpected error occurred. {ex.Message}" });
            }

        }

        [HttpGet("spotify-login")]
        public IActionResult SportifyLogin()
        {

            var spotifyUrl = _spotifyAuthService.SpotifyLogin();

            return Redirect(spotifyUrl);
        }

        [HttpGet("spotify-callback")]
        public async Task<IActionResult> SpotifyCallback([FromQuery] string code, [FromQuery] string state)
        {
            try
            {

                var storedState = Request.Cookies["spotify_state"];
                var flowType = Request.Cookies["flow_type"];

                //Console.WriteLine("Query state: " + state);
                //Console.WriteLine("Cookie state: " + storedState);

                if (string.IsNullOrEmpty(state) || state != storedState)
                {
                    //Console.WriteLine("STATE2: " + state);
                    return Unauthorized("Invalid state: " + state);
                }

                var result = await _spotifyAuthService.SpotifyCallBack(code);

                if (!result.Success)
                {
                    return Unauthorized(new { message = result.Message });
                }

                if (result.SpotifyUserData == null)
                {
                    return StatusCode(500, new { message = "Spotify callback succeeded but no user data was returned." });
                }

                if (flowType == "register")
                {
                    var userId = Request.Cookies["user_id"] ?? "";

                    var Id = Guid.Parse(userId);

                    var user = await _authService.UpdateSpotifyDetailsAsync(Id, result.SpotifyUserData.SpotifyId, result.SpotifyUserData.Email, result.SpotifyUserData.DisplayName);
                    //Console.WriteLine(user.Email);

                    return Redirect("https://opulent-space-giggle-qj7pgx5r9rx3x9pj-5173.app.github.dev/home");
                    //return Ok(user);
                }
                else if (flowType == "login")
                {
                    var user = await _authService.GetUserBySpotifyIdAsync(result.SpotifyUserData.SpotifyId ?? "");

                    return Ok(user);

                }

                return BadRequest(new { message = $"Unknown flow type: '{flowType}'." });


            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Unexpected error: " + ex.Message });
            }

        }

    }
}
