
using AutoMapper;
using infertility_system.Dtos.Booking;
using infertility_system.Dtos.Customer;
using infertility_system.Dtos.Doctor;
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
    using infertility_system.Dtos.ConsulationResult;
    using infertility_system.Dtos.Customer;
    using infertility_system.Dtos.Doctor;
    using infertility_system.Dtos.Email;
    using infertility_system.Dtos.MedicalRecord;
    using infertility_system.Dtos.MedicalRecordDetail;
    using infertility_system.Dtos.OrderDetail;
    using infertility_system.Dtos.TreatmentResult;
    using infertility_system.Dtos.TreatmentRoadmap;
    using infertility_system.Dtos.Typetests;
    using infertility_system.Dtos.TypeTests;
    using infertility_system.Helpers;
    using infertility_system.Interfaces;
    using infertility_system.Middleware;
    using infertility_system.Models;
    using MailKit;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Security.Claims;

    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository doctorRepository;
        private readonly IMedicalRecordRepository medicalRecordRepository;
        private readonly IMedicalRecordDetailRepository medicalRecordDetailRepository;
        private readonly IBookingRepository bookingRepository;
        private readonly IEmailService emailService;
        private readonly ICustomerRepository customerRepository;
        private readonly ITreatementRoadmapRepository treatementRoadmapRepository;
        private readonly IMapper mapper;

        public DoctorController(IDoctorRepository doctorRepository, IMapper mapper, IMedicalRecordRepository medicalRecordRepository, IMedicalRecordDetailRepository medicalRecordDetailRepository, IBookingRepository bookingRepository, IEmailService emailService, ICustomerRepository customerRepository, ITreatementRoadmapRepository treatementRoadmapRepository)
        {
            this.doctorRepository = doctorRepository;
            this.mapper = mapper;
            this.medicalRecordRepository = medicalRecordRepository;
            this.medicalRecordDetailRepository = medicalRecordDetailRepository;
            this.bookingRepository = bookingRepository;
            this.emailService = emailService;
            this.customerRepository = customerRepository;
            this.treatementRoadmapRepository = treatementRoadmapRepository;
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

        [HttpGet("GetListCustomer")]
        public async Task<IActionResult> GetListCustomer()
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var customers = await this.doctorRepository.GetListCustomerAsync(doctorIdClaims);
            return this.Ok(customers);
        }

        [HttpGet("GetListTreatmentRoadmapForCustomer/{customerId}")]
        public async Task<IActionResult> GetListTreatmentRoadmapForCustomer(int customerId)
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var treatmentRoadmaps = await this.doctorRepository.GetTreatmentRoadmapsAsync(doctorIdClaims, customerId);
            var result = this.mapper.Map<List<TreatmentRoadmapDto>>(treatmentRoadmaps);
            return this.Ok(result);
        }

        [HttpGet("GetBookingCustomer/{customerId}")]
        public async Task<IActionResult> GetBookingCustomer(int customerId)
        {
            var bookings = await this.doctorRepository.GetBookingsCustomerAsync(customerId);
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

        [HttpGet("getDetailTreatmentRoadmap/{customerId}/{bookingId}")]
        public async Task<IActionResult> GetDetailTreatmentRoadmap(int customerId, int bookingId)
        {
            var treatmentRoadmapDetail = await this.doctorRepository.GetDetailTreatmentRoadmapAsync(bookingId, customerId);
            var result = this.mapper.Map<List<TreatmentRoadmapDetailDto>>(treatmentRoadmapDetail);
            return this.Ok(result);
        }

        [HttpGet("treatmentResult-typeTest/{customerId}/{bookingId}")]
        public async Task<IActionResult> GetTreatmentResult(int bookingId, int customerId)
        {
            var treatmentResult = await this.doctorRepository.GetTreatmentResultsTypeTestAsync(bookingId, customerId);
            var result = this.mapper.Map<List<TreatmentResultDto>>(treatmentResult);
            return this.Ok(result);
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet("GetMedicalRecordDetail/{medicalRecordId}")]
        public async Task<IActionResult> GetMedicalRecordDetail(int medicalRecordId)
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var medicalRecords = await this.doctorRepository.GetMedicalRecordDetailAsync(doctorIdClaims, medicalRecordId);
            var result = this.mapper.Map<List<MedicalRecordDetailDto>>(medicalRecords);
            return this.Ok(result);
        }

        [HttpGet("consultationResult-typeTests/{customerId}/{bookingId}")]
        public async Task<IActionResult> GetConsultationResultAndTypeTests(int customerId, int bookingId)
        {
            var consulationResults = await this.doctorRepository.GetConsultationResultAndTypeTestsAsync(bookingId, customerId);
            var result = this.mapper.Map<List<ConsultationResultDto>>(consulationResults);
            return this.Ok(result);
        }

        [HttpGet("medicalRecord/{customerId}")]
        public async Task<IActionResult> GetMedicalRecord(int customerId)
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var medicalRecords = await this.doctorRepository.GetMedicalRecordsCustomerAsync(customerId, doctorIdClaims);
            return this.Ok(medicalRecords);
        }

        [HttpGet("appointmentCustomer/{bookingId}")]
        public async Task<IActionResult> GetAppointmentCustomer(int bookingId)
        {
            var orderDetail = await this.doctorRepository.GetListAppointmentCustomerAsync(bookingId);
            var result = this.mapper.Map<List<ListAppointmentDto>>(orderDetail);
            return this.Ok(result);
        }

        [HttpGet("searchCustomerByName/{keyword}")]
        public async Task<IActionResult> SearchCustomerByName(string keyword)
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var customer = await this.doctorRepository.FindCustomerByNameAsync(keyword, doctorIdClaims);
            var result = this.mapper.Map<List<ListCustomerInDoctorDto>>(customer);
            return this.Ok(result);
        }

        [HttpGet("amountCustomer")]
        public async Task<IActionResult> AmountCustomer()
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await this.doctorRepository.AmountCustomerAsync(doctorIdClaims);
            return this.Ok(result);
        }

        [HttpGet("amountMedicalRecord")]
        public async Task<IActionResult> AmountMedicalRecord()
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await this.doctorRepository.AmountMedicalRecordAsync(doctorIdClaims);
            return this.Ok(result);
        }

        [HttpGet("amountMedicalRecordWithStatusComplete")]
        public async Task<IActionResult> AmountMedicalRecordWithStatusComplete()
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await this.doctorRepository.AmountMedicalRecordWithStatusCompleteAsync(doctorIdClaims);
            return this.Ok(result);
        }

        [HttpGet("amountBookingCustomer")]
        public async Task<IActionResult> AmountBookingCustomer()
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await this.doctorRepository.AmountBookingCustomerAsync(doctorIdClaims);
            return this.Ok(result);
        }

        [HttpGet("medicalRecordWithStartDate")]
        public async Task<IActionResult> GetMedicalRecordWithStartDate()
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var medicalRecord = await this.doctorRepository.GetListMedicalRecordWithStartDateAsync(doctorIdClaims);
            var result = this.mapper.Map<List<MedicalRecordWithStartDateDto>>(medicalRecord);
            return this.Ok(result);
        }

        [HttpGet("medicalRecordComplete")]
        public async Task<IActionResult> GetMedicalRecordComplete()
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var medicalRecord = await this.doctorRepository.GetMedicalRecordsCompleteAsync(doctorIdClaims);
            var result = this.mapper.Map<List<MRCustomerNameAndStatusDto>>(medicalRecord);
            return this.Ok(result);
        }

        [HttpGet("medicalRecordInProcess")]
        public async Task<IActionResult> GetMedicalRecordInProcess()
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var medicalRecord = await this.doctorRepository.GetMedicalRecordsInProgressAsync(doctorIdClaims);
            var result = this.mapper.Map<List<MRCustomerNameAndStatusDto>>(medicalRecord);
            return this.Ok(result);
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

        [HttpPost("CreateMedicalRecordDetail/{medicalRecordId}")]
        public async Task<IActionResult> CreateMedicalRecordDetail([FromBody] CreateMedicalRecordDetailDto dto, int medicalRecordId)
        {
            var doctorIdClaims = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var medicalRecordDetail = this.mapper.Map<MedicalRecordDetail>(dto);
            var result = await this.doctorRepository.CreateMedicalRecordDetailAsync(medicalRecordDetail, doctorIdClaims, medicalRecordId);
            return result ? this.Ok("Successfully") : this.BadRequest("Fail");
        }

        [HttpPost("CreateTreatmentResultAndTypeTest/{customerId}")]
        public async Task<IActionResult> CreateTreatmentResultAndTypeTest([FromBody] CreateTreatmentResultAndTypeTestDto dto, int customerId)
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await this.doctorRepository.CreateTreatmentResultAndTypeTestAsync(dto, doctorIdClaims, customerId);
            return result ? this.Ok("Successfully") : this.BadRequest("Fail");
        }

        [HttpPost("typeTest-treatmentResult/{customerId}/{treatmentResultId}")]
        public async Task<IActionResult> CreateTypeTestTreatmentResult([FromBody] CreateTypeTestDto dto, int customerId, int treatmentResultId)
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var typetTest = this.mapper.Map<TypeTest>(dto);

            var result = await this.doctorRepository.CreateTypeTestTreatementResultAsync(typetTest, doctorIdClaims, customerId, treatmentResultId);
            return result ? this.Ok("Successfully") : this.BadRequest("Fail");
        }

        [HttpPost("consultationResult-typeTest/{customerId}/{bookingId}")]
        public async Task<IActionResult> CreateConsultationResultAndTypeTest([FromBody] CreateConsultatioResultAndTypeTestDto dto, int customerId, int bookingId)
        {
            var result = await this.doctorRepository.CreateConsultationAndTypeTestAsync(dto, bookingId, customerId);
            return result ? this.Ok("Successfully") : this.BadRequest("Fail");
        }

        [HttpPost("typeTest-consultationResult/{customerId}/{consultationResultId}")]
        public async Task<IActionResult> CreateTypeTestConsultationResult([FromBody] CreateTypeTestDto dto, int customerId, int consultationResultId)
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var typeTest = this.mapper.Map<TypeTest>(dto);
            var result = await this.doctorRepository.CreateTypeTestConsultationResultAsync(typeTest, doctorIdClaims, customerId, consultationResultId);
            return result ? this.Ok("Successfully") : this.BadRequest("Fail");
        }

        [HttpPost("booking/{bookingId}")]
        public async Task<IActionResult> CreateBookingForCustomer([FromBody] CreateBookingCustomerDto dto, int bookingId)
        {
            var result = await this.doctorRepository.CreateBookingForCustomerAsync(dto, bookingId);
            var stageName = await this.treatementRoadmapRepository.GetStageNameTreatmentRoadmapById(dto.TreatmentRoadmapId);
            var customer = await this.customerRepository.GetCustomerByBookingIdAsync(bookingId);
            var userEmail = customer.Email;
            if (result)
            {
                var emailMessage = new EmailMessage(
                    new List<string> { userEmail },
                    "Thông báo lịch hẹn điều trị.",
                    $"<p>Bạn có lịch hẹn về quy trình <strong>{stageName}</strong> vào ngày <strong>{dto.DateTreatment} lúc {dto.TimeTreatment}.</p>");

                await this.emailService.SendEmail(emailMessage);

                return this.Ok("Success");
            }
            return result ? this.Ok("Successfully") : this.BadRequest("Fail");
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

        [HttpPut("UpdateMedicalRecordDetail/{customerId}/{medicalRecordDetailId}")]
        public async Task<IActionResult> UpdateMedicalRecordDetail([FromBody] UpdateMedicalRecordDetailDto dto
                        ,int customerId, int medicalRecordDetailId)
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var medicalRecordDetail = this.mapper.Map<MedicalRecordDetail>(dto);
            medicalRecordDetail.MedicalRecordDetailId = medicalRecordDetailId;

            var result = await this.doctorRepository.UpdateMedicalRecordDetailAsync(medicalRecordDetail, doctorIdClaims, customerId, medicalRecordDetailId);
            return result ? this.Ok("Successfully") : this.BadRequest("Fail");
        }

        [HttpPut("updateDetailTreatmentRoadmap/{customerId}/{treatmentRoadmapId}")]
        public async Task<IActionResult> UpdateDetailTreatmentRoadmap(int treatmentRoadmapId, int customerId, [FromBody] UpdateDetailTreatmentRoadmapDto dto)
        {
            var updateTreatmentRoadmap = this.mapper.Map<TreatmentRoadmap>(dto);

            var result = await this.doctorRepository.UpdateDetailTreatmentRoadmapAsync(updateTreatmentRoadmap, dto.Status, treatmentRoadmapId, customerId);
            return result ? this.Ok("Successfully") : this.BadRequest("Fail");
        }

        [HttpPut("treatmentResult-typeTest/{treatmentResultId}")]
        public async Task<IActionResult> UpdateTreatmentResultAndTypeTest([FromBody] UpdateTreatmentResultAndTypetestDto dto, int treatmentResultId)
        {
            var result = await this.doctorRepository.UpdateTreatmentResultAndTypeTestAsync(dto, treatmentResultId);
            return result ? this.Ok("Successfully") : this.BadRequest("Fail");
        }

        [HttpPut("consultationResult-typeTest/{consultationResultId}")]
        public async Task<IActionResult> UpdateConsultationResultAndTypeTest([FromBody] UpdateConsultationResultAndTypetestDto dto, int consultationResultId)
        {
            var result = await this.doctorRepository.UpdateConsultationResultAndTypeTestAsync(dto, consultationResultId);
            return result ? this.Ok("Successfully") : this.BadRequest("Fail");
        }

        [HttpPut("updateStatusBooking/{bookingId}")]
        public async Task<IActionResult> UpdateStatusBooking([FromQuery] string status, int bookingId)
        {
            var result = await this.doctorRepository.UpdateStatusBookingAfterCompleteAsync(bookingId, status);
            return result ? this.Ok("Successfully") : this.BadRequest("Fail");
        }
    }
}
