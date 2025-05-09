using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using UserManagementAPI.Contracts;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;


        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        // POST: api/Users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            try
            {
                await _usersService.Register(request.Email, request.Password, request.Username);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        // POST: api/Users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var response = await _usersService.Login(request.Username, request.Password);
            return Ok(new { response });
        }

        // GET: api/Users/getUser
        [Authorize]
        [HttpGet("getUser")]
        public async Task<IActionResult> GetUser()
        {
            var userId = User.FindFirst("userId")?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _usersService.Get(userId);

            return Ok(new { user.Username, user.Email });
            
        }

        


    }
}
