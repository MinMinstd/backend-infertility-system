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
        private readonly IGoogleAuthService googleAuthService;
        private readonly IUserService userService;
        private readonly IJwtService jwtService;

        public AuthController(
            IAuthService authService,
            IGoogleAuthService googleAuthService,
            IUserService userService,
            IJwtService jwtService)
        {
            this.authService = authService;
            this.googleAuthService = googleAuthService;
            this.userService = userService;
            this.jwtService = jwtService;
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

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            try
            {
                // 1. Verify Google token
                var googleUser = await googleAuthService.VerifyGoogleTokenAsync(request.Credential);
                
                if (googleUser == null)
                {
                    return BadRequest(new { message = "Token Google không hợp lệ" });
                }

                // 2. Tìm user trong DB theo email
                var user = await userService.GetByEmailAsync(googleUser.Email);

                if (user == null)
                {
                    return BadRequest(new { message = "Email chưa được đăng ký trong hệ thống" });
                }

                // 3. Tạo JWT token cho hệ thống
                var token = jwtService.GenerateToken(user);

                // 4. Trả về token
                return Ok(new
                {
                    token = token,
                    user = new
                    {
                        userId = user.UserId,
                        email = user.Email,
                        role = user.Role
                    },
                    message = "Đăng nhập Google thành công"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Có lỗi xảy ra khi xác thực Google" });
            }
        }
    }
}
