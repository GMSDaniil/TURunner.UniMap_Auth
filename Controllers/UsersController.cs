using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using UserManagementAPI.Contracts;
using UserManagementAPI.Modells;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;
        
        private readonly EmailService _emailService;


        public UsersController(UsersService usersService, EmailService emailService)
        {
            _usersService = usersService;
            _emailService = emailService;
        }

        // POST: api/Users/register
        [HttpPost("register")]
        [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            try
            {
                await _usersService.Register(request.Email, request.Password, request.Username);
                try
                {
                    var response = await _usersService.Login(request.Username, request.Password);
                    return Ok(response);
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            
        }

        // POST: api/Users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            try
            {
                var response = await _usersService.Login(request.Username, request.Password);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }

            
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

            var user = await _usersService.GetDTO(userId);

            return Ok(user);
            
        }
        
        // POST: api/Users/addFavouriteMeal
        [Authorize]
        [HttpPost("addFavouriteMeal")]
        public async Task<IActionResult> AddFavouriteMeal([FromBody] AddFavouriteMealRequest request)
        {
            var userId = User.FindFirst("userId")?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            try
            {
                var mealDTO = new FavouriteMealDTO(request.Name, request.Price, request.Vegan,
                    request.Vegetarian);
                var id = await _usersService.AddFavouriteMeal(userId, mealDTO);
                return Ok(new {Id = id});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
        // DELETE: api/Users/removeFavouriteMeal
        [Authorize]
        [HttpDelete("removeFavouriteMeal/{id}")]
        public async Task<IActionResult> RemoveFavouriteMeal(int id)
        {
            var userId = User.FindFirst("userId")?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            try
            {
                await _usersService.RemoveFavouriteMeal(userId, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getConfirmationEmail/{email}")]
        public async Task<IActionResult> GetConfirmationEmail(string email)
        {
            try
            {
                await _emailService.SendConfirmationEmail(email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("resetPasswordEmail/{email}")]
        public async Task<IActionResult> ResetPasswordEmail(string email)
        {
            try
            {
                await _emailService.SendPasswordResetEmail(email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
