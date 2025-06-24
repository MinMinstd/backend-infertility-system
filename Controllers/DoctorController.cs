using AutoMapper;
using infertility_system.Dtos.Customer;
using infertility_system.Dtos.Doctor;
using infertility_system.Dtos.MedicalRecord;
using infertility_system.Helpers;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace infertility_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IMedicalRecordDetailRepository _medicalRecordDetailRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        public DoctorController(IDoctorRepository doctorRepository, IMapper mapper, IMedicalRecordRepository medicalRecordRepository, IMedicalRecordDetailRepository medicalRecordDetailRepository, IBookingRepository bookingRepository)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _medicalRecordRepository = medicalRecordRepository;
            _medicalRecordDetailRepository = medicalRecordDetailRepository;
            _bookingRepository = bookingRepository;
        }

        [HttpGet("GetListDoctors")]
        public async Task<IActionResult> GetListDoctors([FromQuery] QueryDoctor query)
        {
            var doctors = await _doctorRepository.GetListDoctorsAsync(query);
            var doctorDto = _mapper.Map<List<DoctorForListDto>>(doctors);
            return Ok(doctorDto);
        }

        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorRepository.GetAllDoctorsAsync();
            var doctorDto = _mapper.Map<List<DoctorForListDto>>(doctors);
            return Ok(doctorDto);
        }

        [HttpGet("GetDoctorById/{doctorId}")]
        public async Task<IActionResult> GetDoctorById(int doctorId)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);
            if (doctor == null)
            {
                return NotFound($"Doctor with ID {doctorId} not found.");
            }
            var doctorDto = _mapper.Map<DoctorDto>(doctor);
            return Ok(doctorDto);
        }

        [HttpGet("GetDoctorsByServiceIdForBookingService/{serviceId}")]
        public async Task<IActionResult> GetDoctorsByServiceId(int serviceId)
        {
            var doctors = await _doctorRepository.GetDoctorsByServiceIdForBookingService(serviceId);
            if (doctors == null || !doctors.Any())
            {
                return NotFound($"No doctors found for service ID {serviceId}.");
            }
            var doctorDto = _mapper.Map<List<DoctorBookingServiceRespondDto>>(doctors);
            return Ok(doctorDto);
        }

        [HttpGet("GetDoctorsByServiceIdForBookingConsulation/{serviceId}")]
        public async Task<IActionResult> GetDoctorsByServiceIdForBookingConsulation(int serviceId)
        {
            var doctors = await _doctorRepository.GetDoctorsByServiceIdForBookingConsulation(serviceId);
            if (doctors == null || !doctors.Any())
            {
                return NotFound($"No doctors found for service ID {serviceId}.");
            }
            var doctorDto = _mapper.Map<List<DoctorBookingConsulationRespondDto>>(doctors);
            return Ok(doctorDto);
        }

        [HttpGet("GetDoctorsForManagement")]
        public async Task<IActionResult> GetDoctosForManagement()
        {
            var doctors = await _doctorRepository.GetDoctosForManagement();
            var doctorDto = _mapper.Map<List<DoctorForManagementDto>>(doctors);
            return Ok(doctorDto);
        }

        //[Authorize(Roles = "Doctor")]
        [HttpPost("CreateMedicalRecord/{customerId}")]
        public async Task<IActionResult> CreateMedicalRecord([FromBody] CreateMedicalRecordDto
            createMedicalRecordDto, int customerId)
        {
            var doctorIdClaim = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var medicalRecord = _mapper.Map<MedicalRecord>(createMedicalRecordDto);
            medicalRecord.CustomerId = customerId;

            var result = await _medicalRecordRepository.
                CreateMedicalRecordAsync(medicalRecord, doctorIdClaim);
            return result ? Ok("Successfully") : BadRequest("Fail");
        }

        [HttpPost("CreateMedicalRecordDetail")]
        public async Task<IActionResult> CreateMedicalRecordDetail([FromBody] MedicalRecordDetailDto dto)
        {
            //var doctorIdClaims = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            //if(!await _doctorRepository.CheckDoctorIdInMedicalRecordAsync(doctorIdClaims, dto.MedicalRecordId))
            //    return Unauthorized("Bạn không có quyền cập nhật hồ sơ này");

            var medicalRecordDetailDto = _mapper.Map<MedicalRecordDetail>(dto);
            var medicalRecordDetail = await _medicalRecordDetailRepository.CreateMedicalRecordDetailAsync(medicalRecordDetailDto);
            return Ok(dto);
        }

        [HttpPut("UpdateMedicalRecord/{medicalRecordId}")]
        public async Task<IActionResult> UpdateMedicalRecord(int medicalRecordId,
            [FromBody] UpdateMedicalRecordDto updateDto)
        {
            var doctorIdClaims = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var medicalRecord = _mapper.Map<MedicalRecord>(updateDto);
            medicalRecord.MedicalRecordId = medicalRecordId;

            var result = await _medicalRecordRepository.
                                UpdateMedicalRecordAsync(medicalRecord, doctorIdClaims);
            return result ? Ok("Successfully") : BadRequest("Fail");
        }


        [HttpGet("GetDoctorsByServiceId/{serviceId}")]
        public async Task<IActionResult> GetDoctorsByServiceId(int serviceId)
        {
            var doctors = await _doctorRepository.GetDoctorsByServiceIdAsync(serviceId);
            if (doctors == null || !doctors.Any())
            {
                return NotFound($"No doctors found for service ID {serviceId}.");
            }
            var doctorDto = _mapper.Map<List<DoctorBookingRespondDto>>(doctors);
            return Ok(doctorDto);
        }

        [HttpGet("GetFullInforCustomer")]
        public async Task<IActionResult> GetFullInforCustomer()
        {
            var doctorIdClaims = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var customers = await _doctorRepository.GetListCustomerFullInforAsync(doctorIdClaims);
            var result = _mapper.Map<List<CustomerInDoctorDto>>(customers);
            return Ok(result);
        }

        [HttpGet("GetMedicalRecordWithDetail/{customerId}")]
        public async Task<IActionResult> GetMedicalRecordWithDetail(int customerId)
        {
            var doctorIdClaims = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var medicalRecords = await _doctorRepository.GetMedicalRecordWithDetailAsync(doctorIdClaims, customerId);
            var result = _mapper.Map<List<MedicalRecordWithDetailDto>>(medicalRecords);
            return Ok(result);
        }

    }
}
