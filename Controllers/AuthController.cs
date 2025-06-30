namespace infertility_system.Controllers
{
    using infertility_system.Dtos.Email;
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
        private readonly IEmailService emailService;

        public AuthController(
            IAuthService authService,
            IGoogleAuthService googleAuthService,
            IUserService userService,
            IJwtService jwtService,
            IEmailService emailService)
        {
            this.authService = authService;
            this.googleAuthService = googleAuthService;
            this.userService = userService;
            this.jwtService = jwtService;
            this.emailService = emailService;
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

            var confirmLink = $"http://localhost:5173/confirm-email?token={newUser.TokenConfirmation}";
            var emailBody = $"<p>Cảm ơn bạn đã đăng kí, hãy nhấn vào <a href='{confirmLink}'>link xác nhận này</a> để tài khoản bạn được kích hoạt.</p>";
            var emailMessage = new EmailMessage(
                new List<string> { newUser.Email },
                "Welcome to Infertility System",
                emailBody);
            await this.emailService.SendEmail(emailMessage);
            return this.Ok(new { message = "Registration successful, go check email to active your account", userId = newUser.UserId });
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            try
            {
                // 1. Verify Google token
                var googleUser = await this.googleAuthService.VerifyGoogleTokenAsync(request.Credential);

                if (googleUser == null)
                {
                    return this.BadRequest(new { message = "Token Google không hợp lệ" });
                }

                // 2. Tìm user trong DB theo email
                var user = await this.userService.GetByEmailAsync(googleUser.Email);

                if (user == null)
                {
                    return this.BadRequest(new { message = "Email chưa được đăng ký trong hệ thống" });
                }

                // 3. Tạo JWT token cho hệ thống
                var token = this.jwtService.GenerateToken(user);

                // 4. Trả về token
                return this.Ok(new
                {
                    token = token,
                    user = new
                    {
                        userId = user.UserId,
                        email = user.Email,
                        role = user.Role,
                    },
                    message = "Đăng nhập Google thành công",
                });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { message = "Có lỗi xảy ra khi xác thực Google" });
            }
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
        {
            var result = await this.authService.ConfirmEmailAsync(token);
            if (!result)
            {
                return this.BadRequest(new { message = "Token không hợp lệ hoặc tài khoản đã được xác nhận." });
            }

            return this.Ok(new { message = "Xác nhận email thành công, tài khoản đã được kích hoạt." });
        }
    }
}
