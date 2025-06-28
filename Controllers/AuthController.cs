namespace infertility_system.Controllers
{
    using infertility_system.Dtos.User;
    using infertility_system.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var token = await this.authService.AuthenticateUserAsync(loginRequest);
            if (token == null)
            {
                return this.Unauthorized(new { message = "Invalid username or password." });
            }

            return this.Ok(new { token = token, message = "Login successful." });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto user)
        {
            var newUser = await this.authService.RegisterUserAsync(user);
            if (newUser == null)
            {
                return this.BadRequest(new { message = "User already exists or registration failed." });
            }

            return this.Ok(new { message = "Registration successful.", userId = newUser.UserId });
        }
    }
}
