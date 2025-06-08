using infertility_system.Dtos.User;
using infertility_system.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace infertility_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var token = await _authService.AuthenticateUserAsync(loginRequest);
            if (token == null)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }
            return Ok(new { token = token, message = "Login successful." });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto user)
        {
            var newUser = await _authService.RegisterUserAsync(user);
            if (newUser == null)
            {
                return BadRequest(new { message = "User already exists or registration failed." });
            }
            return Ok(new { message = "Registration successful.", userId = newUser.UserId });
        }
    }
}
