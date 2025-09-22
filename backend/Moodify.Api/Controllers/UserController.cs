using Microsoft.AspNetCore.Mvc;
using Moodify.Api.Models;
using Moodify.Api.Services.IServices;
using Moodify.Api.Dtos;


namespace Moodify.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(user);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        //Get: api/user/email/{email}
        [HttpGet("email/{email}")]
        public async Task<IActionResult> getUserByEmail(string Email)
        {
            try
            {
                var user = await _userService.GetUserByEmailAsync(Email.ToLower().Trim());

                if (user == null)
                {
                    return NotFound("User not found");
                }

                return Ok(user);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


    }
}
