
﻿using System.Security.Claims;
using AutoMapper;
using infertility_system.Dtos.Doctor;
using infertility_system.Dtos.MedicalRecord;
using infertility_system.Dtos.Doctor;
using infertility_system.Helpers;
using infertility_system.Interfaces;
using infertility_system.Mappers;
using infertility_system.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace infertility_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;


        public DoctorController(IDoctorRepository doctorRepository,IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAllDoctors")]
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
            if (!await _doctorRepository.CheckCustomerInBooking(customerId))
                return NotFound("Customer not found in booking");

            var doctorIdClaim = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if(doctorIdClaim == null)
                return Unauthorized("Doctor Id not found");

            var medicalRecordDto = _mapper.Map<MedicalRecord>(createMedicalRecordDto);
            medicalRecordDto.CustomerId = customerId;
            medicalRecordDto.DoctorId = doctorIdClaim;

            var medicalRecord = await _doctorRepository.CreateMedicalRecordAsync(medicalRecordDto);

            return Ok(medicalRecord);
        }

        [HttpPut("UpdateMedicalRecord/{medicalRecordId}/{customerId}")]
        public async Task<IActionResult> UpdateMedicalRecord(int customerId, int medicalRecordId, 
            [FromBody] UpdateMedicalRecordDto updateDto)
        {
            //kiểm tra booking
            if (!await _doctorRepository.CheckCustomerInBooking(customerId))
                return NotFound("Customer not found in booking");

            //kiểm tra doctor có trong medicalRecord
            var doctorIdClaims = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if(!await _doctorRepository.CheckDoctorIdInMedicalRecord(doctorIdClaims, medicalRecordId))
                return Unauthorized("Bạn không có quyền cập nhật hồ sơ này");

            var recordMap = _mapper.Map<MedicalRecord>(updateDto);
            recordMap.CustomerId = customerId;
            recordMap.MedicalRecordId = medicalRecordId;

            var update = await _doctorRepository.UpdateMedicalRecordAsync(medicalRecordId, recordMap);
            return Ok(_mapper.Map<UpdateMedicalRecordDto>(update));
        }
    }
}
