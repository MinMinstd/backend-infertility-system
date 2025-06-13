using System.Security.Claims;
using AutoMapper;
using infertility_system.Dtos.Doctor;
using infertility_system.Dtos.MedicalRecord;
using infertility_system.Helpers;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using infertility_system.Models;

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
        public async Task<IActionResult> GetAllDoctors([FromQuery] QueryDoctor query)
        {
            var doctors = await _doctorRepository.GetAllDoctorsAsync(query);
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

        [HttpPost("CreateMedicalRecord/{customerId}")]
        public async Task<IActionResult> CreateMedicalRecord([FromBody] CreateMedicalRecordDto
            createMedicalRecordDto, int customerId)
        {
            if (!await _bookingRepository.CheckCustomerInBookingAsync(customerId))
                return NotFound("Customer not found in booking");

            var doctorIdClaim = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (doctorIdClaim == null)
                return Unauthorized("Doctor Id not found");

            var medicalRecordDto = _mapper.Map<MedicalRecord>(createMedicalRecordDto);
            medicalRecordDto.CustomerId = customerId;
            medicalRecordDto.DoctorId = doctorIdClaim;

            var medicalRecord = await _medicalRecordRepository.CreateMedicalRecordAsync(medicalRecordDto);

            return Ok(medicalRecord);
        }

        [HttpPut("UpdateMedicalRecord/{medicalRecordId}/{customerId}")]
        public async Task<IActionResult> UpdateMedicalRecord(int customerId, int medicalRecordId,
            [FromBody] UpdateMedicalRecordDto updateDto)
        {
            //kiểm tra booking
            if (!await _bookingRepository.CheckCustomerInBookingAsync(customerId))
                return NotFound("Customer not found in booking");

            //kiểm tra doctor có trong medicalRecord
            var doctorIdClaims = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (!await _medicalRecordRepository.CheckDoctorIdInMedicalRecordAsync(doctorIdClaims, medicalRecordId))
                return Unauthorized("Bạn không có quyền cập nhật hồ sơ này");

            var recordMap = _mapper.Map<MedicalRecord>(updateDto);
            recordMap.CustomerId = customerId;
            recordMap.MedicalRecordId = medicalRecordId;

            var update = await _medicalRecordRepository.UpdateMedicalRecordAsync(medicalRecordId, recordMap);
            return Ok(_mapper.Map<UpdateMedicalRecordDto>(update));
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
