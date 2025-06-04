using infertility_system.Data;
using infertility_system.Dtos.Customer;
using infertility_system.Dtos.User;
using infertility_system.Interfaces;
using infertility_system.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace infertility_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized(new { Message = "Token không hợp lệ hoặc thiếu UserId!" });
                }

                var customers = await _customerRepository.GetCustomersAsync(userId);

                if (customers == null || !customers.Any())
                {
                    return NotFound(new { Message = "Không tìm thấy dữ liệu khách hàng!" });
                }

                return Ok(customers.Select(c => c.ToCustomerDto()));
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { Message = "Token không hợp lệ hoặc thiếu UserId!" });
            }
            if (dto == null)
            {
                return BadRequest(new { Message = "Dữ liệu không hợp lệ!" });
            }
            var result = await _customerRepository.ChangePasswordAsync(userId, dto);
            if (!result)
            {
                return BadRequest(new { Message = "Đổi mật khẩu không thành công!" });
            }
            return Ok(new { Message = "Đổi mật khẩu thành công!" });
        }
    }

}
