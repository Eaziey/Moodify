using Microsoft.AspNetCore.Mvc;
using Moodify.Api.Models;
using Moodify.Api.Services.IServices;
using Moodify.Api.Dtos;
using System.Security;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

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

        [AllowAnonymous]
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

                var token = await _authService.VarifyUserLoginDetailsAsync(user);

                
                if (string.IsNullOrEmpty(token))
                {
                    return StatusCode(500, new { message = "Unexpected error: login token was null or empty." });
                }


                Response.Cookies.Append("jwt", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddHours(2)
                });
                
                return StatusCode(201, new {message = "Login successful."});
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

        [AllowAnonymous]
        [HttpPost("signIn")]
        public async Task<IActionResult> SignInUser([FromBody] UserRegistrationDto user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null.");
            }

            try
            {

                var _userDataAndToken = await _authService.AddUserAsync(user);

                var token = _userDataAndToken.Token;
                var _user = _userDataAndToken.User;

                if (string.IsNullOrEmpty(token))
                {
                    return StatusCode(500, new { message = "Unexpected error: signIn token was null or empty." });
                }


                Response.Cookies.Append("jwt", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddHours(2)
                });


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

                    /*Response.Cookies.Append("token", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(10)
                    });*/

                    var redirectUrl = _spotifyAuthService.GetSpotifyUrl(state);
                    return Ok(new { redirectUrl });

                }

                //return Redirect("https://opulent-space-giggle-qj7pgx5r9rx3x9pj-5173.app.github.dev/home?token={token}");

                return StatusCode(201,new {message = "Sign in successful"});
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

        [AllowAnonymous]
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

                    //var token = Request.Cookies["jwt"] ?? "";

                    var Id = Guid.Parse(userId);

                    var isUserUpdated = await _authService.UpdateSpotifyDetailsAsync(Id, result.SpotifyUserData.SpotifyId, result.SpotifyUserData.Email, result.SpotifyUserData.DisplayName);

                    return StatusCode(201, new { message = "Sign in successful." });
                }
                else if (flowType == "login")
                {
                    var token = await _authService.GetUserTokenBySpotifyIdAsync(result.SpotifyUserData.SpotifyId ?? "");

                    if (string.IsNullOrEmpty(token))
                    {
                        return StatusCode(500, new { message = "Unexpected error: login token was null or empty." });
                    }

                    Response.Cookies.Append("jwt", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTimeOffset.UtcNow.AddHours(2)
                    });

                    return StatusCode(201, new { message = "Login successful." });

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

        
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "Logged out successfully" });
        }


    }
}
