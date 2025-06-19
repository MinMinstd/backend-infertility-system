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
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper, IMedicalRecordDetailRepository medicalRecordDetailRepository, IEmbryoRepository embryoRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _medicalRecordDetailRepository = medicalRecordDetailRepository;
            _embryoRepository = embryoRepository;
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

            if (customers == null)
            {
                return NotFound(new { Message = "Không tìm thấy dữ liệu khách hàng!" });
            }

            return Ok(_mapper.Map<List<CustomerDto>>(customers));
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
    }

}
