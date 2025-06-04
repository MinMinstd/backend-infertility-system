using infertility_system.Data;
using infertility_system.Dtos.Admin;
using infertility_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace infertility_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterRequestFromAdminDto dto)
        {
            if (dto.Role != "Manager" && dto.Role != "Doctor")
                return BadRequest("Admin chỉ có thể tạo Manager hoặc Doctor!");

            var newUser = new User
            {
                Email = dto.Email,
                Phone = dto.Phone,
                Password = dto.Password,
                Role = dto.Role
            };  

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            if (dto.Role == "Doctor")
            {
                var newDoctor = new Doctor
                {
                    UserId = newUser.UserId,
                    FullName = dto.FullName,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Experience = dto.Experience
                };
                _context.Doctors.Add(newDoctor);
            }
            else if (dto.Role == "Manager")
            {
                var newManager = new Manager
                {
                    UserId = newUser.UserId,
                    FullName = dto.FullName,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Address = dto.Address
                };
                _context.Managers.Add(newManager);
            }

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Tài khoản đã được tạo thành công!" });
        }
    }
}
