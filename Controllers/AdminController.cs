using infertility_system.Data;
using infertility_system.Dtos.Admin;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace infertility_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AdminController(IAuthService authService)
        {
            _authService = authService;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterRequestFromAdminDto dto)
        {
            var result = await _authService.RegisterDoctorAndManagerAsync(dto);
            return Ok(result);
        }
    }
}
