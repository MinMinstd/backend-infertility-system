namespace infertility_system.Controllers
{
    using AutoMapper;
    using infertility_system.Dtos.Booking;
    using infertility_system.Dtos.Customer;
    using infertility_system.Dtos.Doctor;
    using infertility_system.Dtos.Embryo;
    using infertility_system.Dtos.MedicalRecord;
    using infertility_system.Dtos.MedicalRecordDetail;
    using infertility_system.Dtos.OrderDetail;
    using infertility_system.Dtos.User;
    using infertility_system.Interfaces;
    using infertility_system.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMedicalRecordDetailRepository medicalRecordDetailRepository;
        private readonly IEmbryoRepository embryoRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public CustomerController(
            ICustomerRepository customerRepository,
            IMapper mapper,
            IMedicalRecordDetailRepository medicalRecordDetailRepository,
            IEmbryoRepository embryoRepository,
            IUserRepository userRepository)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
            this.medicalRecordDetailRepository = medicalRecordDetailRepository;
            this.embryoRepository = embryoRepository;
            this.userRepository = userRepository;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomer()
        {
            var userIdClaim = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return this.Unauthorized(new { Message = "Token không hợp lệ hoặc thiếu UserId!" });
            }

            var customer = await this.customerRepository.GetCustomersAsync(userId);

            if (customer == null)
            {
                return this.NotFound(new { Message = "Không tìm thấy dữ liệu khách hàng!", Id = userIdClaim });
            }

            return this.Ok(this.mapper.Map<CustomerDto>(customer));
        }

        [HttpGet("medicalRecord")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetMedicalRecord()
        {
            var userIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var medicalRecords = await this.medicalRecordDetailRepository.GetMedicalRecordAsync(userIdClaims);

            var dto = this.mapper.Map<List<MedicalRecordDto>>(medicalRecords);
            return this.Ok(dto);
        }

        [HttpGet("medicalRecordDetail-treatmentResult-typeTest/{medicalRecordId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetMedicalRecordDetailWithTreatmentResultAndTypeTest(int medicalRecordId)
        {

            var medicalRecordDetails = await this.medicalRecordDetailRepository.
                        GetMedicalRecordDetailWithTreatmentResultAndTypetestAsync(medicalRecordId);

            var result = this.mapper.Map<List<MedicalRecordDetailWithTreatmentResultAndTypeTestDto>>(medicalRecordDetails);

            return this.Ok(result);
        }

        [HttpGet("embryos")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetEmbryos()
        {
            var userIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (!await this.customerRepository.CheckCustomerExistsAsync(userIdClaims))
            {
                return this.NotFound();
            }

            var embryos = await this.embryoRepository.GetListEmbryosAsync(userIdClaims);
            var embryosDto = this.mapper.Map<List<EmbryoDto>>(embryos);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            return this.Ok(embryosDto);
        }

        [HttpPut("ChangePassword")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var userIdClaim = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return this.Unauthorized(new { Message = "Token không hợp lệ hoặc thiếu UserId!" });
            }

            if (dto == null)
            {
                return this.BadRequest(new { Message = "Dữ liệu không hợp lệ!" });
            }

            var result = await this.customerRepository.ChangePasswordAsync(userId, dto);
            if (!result)
            {
                return this.BadRequest(new { Message = "Đổi mật khẩu không thành công!" });
            }

            return this.Ok(new { Message = "Đổi mật khẩu thành công!" });
        }

        [HttpPut("UpdateCustomerProfile")]
        public async Task<IActionResult> UpdateCustomerProfile(CustomerProfileDto customerProfileDto)
        {
            var userIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var exitCustomer = await this.customerRepository.GetCustomersAsync(userIdClaims);
            if (exitCustomer == null)
            {
                return this.NotFound(new { Message = "Không tìm thấy dữ liệu khách hàng!" });
            }

            customerProfileDto.FullName = customerProfileDto.FullName.Any() ? customerProfileDto.FullName : exitCustomer.FullName;
            customerProfileDto.Email = customerProfileDto.Email.Any() ? customerProfileDto.Email : exitCustomer.Email;
            customerProfileDto.Phone = customerProfileDto.Phone.Any() ? customerProfileDto.Phone : exitCustomer.Phone;
            customerProfileDto.Gender = customerProfileDto.Gender.Any() ? customerProfileDto.Gender : exitCustomer.Gender;
            customerProfileDto.Birthday = customerProfileDto.Birthday != null ? customerProfileDto.Birthday : exitCustomer.Birthday;
            customerProfileDto.Address = customerProfileDto.Address.Any() ? customerProfileDto.Address : exitCustomer.Address;

            var user = this.mapper.Map<User>(customerProfileDto);
            var updatedUser = await this.userRepository.UpdateUser(userIdClaims, user);
            if (updatedUser == null)
            {
                return this.BadRequest(new { Message = "Cập nhật thông tin người dùng không thành công!" });
            }

            var customer = this.mapper.Map<Customer>(customerProfileDto);
            var updatedCustomer = await this.customerRepository.UpdateCutomerAsync(userIdClaims, customer);
            if (updatedCustomer == null)
            {
                return this.BadRequest(new { Message = "Cập nhật thông tin khách hàng không thành công!" });
            }

            return this.Ok(new { Message = "Cập nhật thông tin thành công!", Customer = this.mapper.Map<CustomerDto>(updatedCustomer) });
        }

        [HttpGet("GetListDoctors")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetListDoctor()
        {
            var doctors = await this.customerRepository.GetListDoctorsAsync();
            var result = this.mapper.Map<List<ListDoctorsDto>>(doctors);
            return this.Ok(result);
        }

        [HttpGet("GetDoctorDetail/{doctorId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetDoctorDetail(int doctorId)
        {
            var doctorDetail = await this.customerRepository.GetDoctorDetailAsync(doctorId);
            var result = this.mapper.Map<DoctorDetailDto>(doctorDetail);
            return this.Ok(result);
        }

        [HttpGet("GetListBookingInCustomer")]
        public async Task<IActionResult> GetListBookingInCustomer()
        {
            var userIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var bookings = await this.customerRepository.GetBookingsAsync(userIdClaims);

            var result = this.mapper.Map<List<BookingInCustomerDto>>(bookings);
            return this.Ok(result);
        }

        [HttpGet("getInformationService")]
        public async Task<IActionResult> GetInformationService()
        {
            var userIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var medicalRecord = await this.customerRepository.GetInformationServiceAsync(userIdClaims);
            var result = this.mapper.Map<List<UseServiceByCustomerDto>>(medicalRecord);
            return this.Ok(result);
        }

        [HttpGet("getListAppointment/{bookingId}")]
        public async Task<IActionResult> GetListAppointment(int bookingId)
        {
            var orderDetail = await this.customerRepository.GetListAppointmentAsync(bookingId);
            var result = this.mapper.Map<List<ListAppointmentDto>>(orderDetail);
            return this.Ok(result);
        }
    }
}
