
﻿using AutoMapper;
using infertility_system.Dtos.Booking;
using infertility_system.Dtos.Customer;
using infertility_system.Dtos.Doctor;
using infertility_system.Dtos.MedicalRecord;
using infertility_system.Dtos.TreatmentRoadmap;
using infertility_system.Helpers;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace infertility_system.Controllers

{
    using AutoMapper;
    using infertility_system.Dtos.Booking;
    using infertility_system.Dtos.Customer;
    using infertility_system.Dtos.Doctor;
    using infertility_system.Dtos.MedicalRecord;
    using infertility_system.Dtos.TreatmentRoadmap;
    using infertility_system.Helpers;
    using infertility_system.Interfaces;
    using infertility_system.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository doctorRepository;
        private readonly IMedicalRecordRepository medicalRecordRepository;
        private readonly IMedicalRecordDetailRepository medicalRecordDetailRepository;
        private readonly IBookingRepository bookingRepository;
        private readonly IMapper mapper;

        public DoctorController(IDoctorRepository doctorRepository, IMapper mapper, IMedicalRecordRepository medicalRecordRepository, IMedicalRecordDetailRepository medicalRecordDetailRepository, IBookingRepository bookingRepository)
        {
            this.doctorRepository = doctorRepository;
            this.mapper = mapper;
            this.medicalRecordRepository = medicalRecordRepository;
            this.medicalRecordDetailRepository = medicalRecordDetailRepository;
            this.bookingRepository = bookingRepository;
        }

        [HttpGet("GetListDoctors")]
        public async Task<IActionResult> GetListDoctors([FromQuery] QueryDoctor query)
        {
            var doctors = await this.doctorRepository.GetListDoctorsAsync(query);
            var doctorDto = this.mapper.Map<List<DoctorForListDto>>(doctors);
            return this.Ok(doctorDto);
        }

        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await this.doctorRepository.GetAllDoctorsAsync();
            var doctorDto = this.mapper.Map<List<DoctorForListDto>>(doctors);
            return this.Ok(doctorDto);
        }

        [HttpGet("GetDoctorById/{doctorId}")]
        public async Task<IActionResult> GetDoctorById(int doctorId)
        {
            var doctor = await this.doctorRepository.GetDoctorByIdAsync(doctorId);
            if (doctor == null)
            {
                return this.NotFound($"Doctor with ID {doctorId} not found.");
            }

            var doctorDto = this.mapper.Map<DoctorDto>(doctor);
            return this.Ok(doctorDto);
        }

        [HttpGet("GetDoctorsByServiceIdForBookingService/{serviceId}")]
        public async Task<IActionResult> GetDoctorsByServiceId(int serviceId)
        {
            var doctors = await this.doctorRepository.GetDoctorsByServiceIdForBookingService(serviceId);
            if (doctors == null || !doctors.Any())
            {
                return this.NotFound($"No doctors found for service ID {serviceId}.");
            }

            var doctorDto = this.mapper.Map<List<DoctorBookingServiceRespondDto>>(doctors);
            return this.Ok(doctorDto);
        }

        [HttpGet("GetDoctorsByServiceIdForBookingConsulation/{serviceId}")]
        public async Task<IActionResult> GetDoctorsByServiceIdForBookingConsulation(int serviceId)
        {
            var doctors = await this.doctorRepository.GetDoctorsByServiceIdForBookingConsulation(serviceId);
            if (doctors == null || !doctors.Any())
            {
                return this.NotFound($"No doctors found for service ID {serviceId}.");
            }

            var doctorDto = this.mapper.Map<List<DoctorBookingConsulationRespondDto>>(doctors);
            return this.Ok(doctorDto);
        }

        [HttpGet("GetDoctorsForManagement")]
        public async Task<IActionResult> GetDoctosForManagement()
        {
            var doctors = await this.doctorRepository.GetDoctosForManagement();
            var doctorDto = this.mapper.Map<List<DoctorForManagementDto>>(doctors);
            return this.Ok(doctorDto);
        }

        // [Authorize(Roles = "Doctor")]
        [HttpPost("CreateMedicalRecord/{customerId}")]
        public async Task<IActionResult> CreateMedicalRecord(
            [FromBody] CreateMedicalRecordDto
            createMedicalRecordDto, int customerId)
        {
            var doctorIdClaim = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var medicalRecord = this.mapper.Map<MedicalRecord>(createMedicalRecordDto);
            medicalRecord.CustomerId = customerId;

            var result = await this.medicalRecordRepository.
                CreateMedicalRecordAsync(medicalRecord, doctorIdClaim);
            return result ? this.Ok("Successfully") : this.BadRequest("Fail");
        }

        [HttpPost("CreateMedicalRecordDetail")]
        public async Task<IActionResult> CreateMedicalRecordDetail([FromBody] MedicalRecordDetailDto dto)
        {
            // var doctorIdClaims = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            // if(!await _doctorRepository.CheckDoctorIdInMedicalRecordAsync(doctorIdClaims, dto.MedicalRecordId))
            //    return Unauthorized("Bạn không có quyền cập nhật hồ sơ này");
            var medicalRecordDetailDto = this.mapper.Map<MedicalRecordDetail>(dto);
            var medicalRecordDetail = await this.medicalRecordDetailRepository.CreateMedicalRecordDetailAsync(medicalRecordDetailDto);
            return this.Ok(dto);
        }

        [HttpPut("UpdateMedicalRecord/{medicalRecordId}")]
        public async Task<IActionResult> UpdateMedicalRecord(
            int medicalRecordId,
            [FromBody] UpdateMedicalRecordDto updateDto)
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var medicalRecord = this.mapper.Map<MedicalRecord>(updateDto);
            medicalRecord.MedicalRecordId = medicalRecordId;

            var result = await this.medicalRecordRepository.
                                UpdateMedicalRecordAsync(medicalRecord, doctorIdClaims);
            return result ? this.Ok("Successfully") : this.BadRequest("Fail");
        }

        [HttpGet("GetListCustomer")]
        public async Task<IActionResult> GetListFullInforCustomer()
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var customers = await this.doctorRepository.GetListCustomerAsync(doctorIdClaims);
            var result = this.mapper.Map<List<ListCustomerInDoctorDto>>(customers);
            return this.Ok(result);
        }

        [HttpGet("GetMedicalRecordWithDetail/{customerId}")]
        public async Task<IActionResult> GetMedicalRecordWithDetail(int customerId)
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var medicalRecords = await this.doctorRepository.GetMedicalRecordWithDetailAsync(doctorIdClaims, customerId);
            var result = this.mapper.Map<List<MedicalRecordWithDetailDto>>(medicalRecords);
            return this.Ok(result);
        }

        [HttpGet("GetMedicalRecordDetailWithTreatmentResultAndTypeTest/{customerId}")]
        public async Task<IActionResult> GetMRDWithTreatmentResultAndTypeTest(int customerId)
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var medicalRecordDetails = await this.doctorRepository.
                GetMedicalRecordDetailWithTreatmentResultAndTypeTestAsync(doctorIdClaims, customerId);
            var result = this.mapper.Map<List<MedicalRecordDetailWithTreatmentResultAndTypeTestDto>>(medicalRecordDetails);
            return this.Ok(result);
        }

        [HttpGet("GetListTreatmentRoadmapForCustomer/{customerId}")]
        public async Task<IActionResult> GetListTreatmentRoadmapForCustomer(int customerId)
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var treatmentRoadmaps = await this.doctorRepository.GetTreatmentRoadmapsAsync(doctorIdClaims, customerId);
            var result = this.mapper.Map<List<TreatmentRoadmapDto>>(treatmentRoadmaps);
            return this.Ok(result);
        }

        [HttpGet("GetBookingCustomer")]
        public async Task<IActionResult> GetBookingCustomer()
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var bookings = await this.doctorRepository.GetBookingsCustomerAsync(doctorIdClaims);
            var result = this.mapper.Map<List<BookingCustomerDto>>(bookings);
            return this.Ok(result);
        }

        [HttpGet("getPatientInformation/{customerId}")]
        public async Task<IActionResult> GetPatientInformation(int customerId)
        {
            var patientInformation = await this.doctorRepository.GetPatientInformationAsync(customerId);
            var result = this.mapper.Map<PatientInformationDto>(patientInformation);
            return this.Ok(result);
        }
    }
}
