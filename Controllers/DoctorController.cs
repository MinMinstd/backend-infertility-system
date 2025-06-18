using System.Security.Claims;
using AutoMapper;
using infertility_system.Dtos.Doctor;
using infertility_system.Dtos.MedicalRecord;
using infertility_system.Helpers;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


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
        public DoctorController(IDoctorRepository doctorRepository,IMapper mapper, IMedicalRecordRepository medicalRecordRepository, IMedicalRecordDetailRepository medicalRecordDetailRepository, IBookingRepository bookingRepository)
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


    }
}
