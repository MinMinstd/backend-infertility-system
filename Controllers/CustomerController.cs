using AutoMapper;
using infertility_system.Dtos.Customer;
using infertility_system.Dtos.MedicalRecord;
using infertility_system.Dtos.User;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace infertility_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMedicalRecordDetailRepository _medicalRecordDetailRepository;
        private readonly IEmbryoRepository _embryoRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CustomerController(
            ICustomerRepository customerRepository, 
            IMapper mapper,
            IMedicalRecordDetailRepository medicalRecordDetailRepository,
            IEmbryoRepository embryoRepository,
            IUserRepository userRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _medicalRecordDetailRepository = medicalRecordDetailRepository;
            _embryoRepository = embryoRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomer()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { Message = "Token không hợp lệ hoặc thiếu UserId!" });
            }

            var customer = await _customerRepository.GetCustomersAsync(userId);

            if (customer == null)
            {
                return NotFound(new { Message = "Không tìm thấy dữ liệu khách hàng!", Id = userIdClaim });
            }

            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        [HttpPut("ChangePassword")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
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

        [HttpGet("medicalRecordWithDetail")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetMedicalRecordsWithDetail()
        {
            var userIdClaims = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var medicalRecords = await _medicalRecordDetailRepository.GetMedicalRecordWithDetailsAsync(userIdClaims);
            var recordsDto = _mapper.Map<List<MedicalRecordWithDetailDto>>(medicalRecords);

            return Ok(recordsDto);
        }

        [HttpGet("medicalRecord")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetMedicalRecord()
        {
            var userIdClaims = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var medicalRecords = await _medicalRecordDetailRepository.GetMedicalRecordAsync(userIdClaims);

            var dto = _mapper.Map<List<MedicalRecordDto>>(medicalRecords);
            return Ok(dto);
        }
        
        [HttpGet("embryos")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetEmbryos()
        {
            var userIdClaims = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (!await _customerRepository.CheckCustomerExistsAsync(userIdClaims))
                return NotFound();

            var embryos = await _embryoRepository.GetListEmbryosAsync(userIdClaims);
            var embryosDto = _mapper.Map<List<EmbryoDto>>(embryos);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(embryosDto);
        }

        [HttpPut("UpdateCustomerProfile")]
        
        public async Task<IActionResult> UpdateCustomerProfile(CustomerProfileDto customerProfileDto)
        {
            var UserIdClaims = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var exitCustomer = await _customerRepository.GetCustomersAsync(UserIdClaims);
            if (exitCustomer == null)
                return NotFound(new { Message = "Không tìm thấy dữ liệu khách hàng!" });

            customerProfileDto.FullName = customerProfileDto.FullName.Any() ? customerProfileDto.FullName : exitCustomer.FullName;
            customerProfileDto.Email = customerProfileDto.Email.Any() ? customerProfileDto.Email : exitCustomer.Email;
            customerProfileDto.Phone = customerProfileDto.Phone.Any() ? customerProfileDto.Phone : exitCustomer.Phone;
            customerProfileDto.Gender = customerProfileDto.Gender.Any() ? customerProfileDto.Gender : exitCustomer.Gender;
            customerProfileDto.Birthday = customerProfileDto.Birthday != null ? customerProfileDto.Birthday : exitCustomer.Birthday;
            customerProfileDto.Address = customerProfileDto.Address.Any() ? customerProfileDto.Address : exitCustomer.Address;

            var user = _mapper.Map<User>(customerProfileDto);
            var updatedUser = await _userRepository.UpdateUser(UserIdClaims, user);
            if (updatedUser == null)
            {
                return BadRequest(new { Message = "Cập nhật thông tin người dùng không thành công!" });
            }




            var customer = _mapper.Map<Customer>(customerProfileDto);
            var updatedCustomer = await _customerRepository.UpdateCutomerAsync(UserIdClaims, customer);
            if (updatedCustomer == null)
            {
                return BadRequest(new { Message = "Cập nhật thông tin khách hàng không thành công!" });
            }
            return Ok(new { Message = "Cập nhật thông tin thành công!", Customer = _mapper.Map<CustomerDto>(updatedCustomer) });

        }
    }

}
