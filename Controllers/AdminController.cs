namespace infertility_system.Controllers
{
    using infertility_system.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAuthService authService;

        public AdminController(IAuthService authService)
        {
            this.authService = authService;
        }

        //// [Authorize(Roles = "Admin")]
        //[HttpPost("create-user")]
        //public async Task<IActionResult> CreateUser([FromBody] RegisterRequestFromAdminDto dto)
        //{
        //    var result = await this.authService.RegisterDoctorAndManagerAsync(dto);
        //    return this.Ok(result);
        //}
    }
}
