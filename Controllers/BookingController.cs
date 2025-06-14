using AutoMapper;
using infertility_system.Dtos.Booking;
using infertility_system.Dtos.Doctor;
using infertility_system.Dtos.DoctorSchedule;
using infertility_system.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace infertility_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctor()
        {
            var doctors = await _bookingRepository.GetAllDoctorAsync();
            var doctorDto = _mapper.Map<List<DoctorForListDto>>(doctors);
            return Ok(doctorDto);
        }

        [HttpGet("GetListDoctorSchedule/{doctorId}")]
        public async Task<IActionResult> GetAllDoctorScheduleByDoctorId(int doctorId, [FromQuery] DateOnly date)
        {
            var doctorSchedules = await _bookingRepository.GetDoctorScheduleAsync(doctorId, date);
            var doctorScheduleDtos = _mapper.Map<List<DoctorScheduleDto>>(doctorSchedules);
            return Ok(doctorScheduleDtos);
        }

        [HttpPost("booking_consulant")]
        public async Task<IActionResult> CreateBookingConsultant([FromBody] BookingConsulantDto bookingDto)
        {
            var userIdClaim = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var book = await _bookingRepository.BookingConsulantAsync(bookingDto, userIdClaim);
            return book ? Ok("Success") : BadRequest("Fail");
        }

        [HttpPost("booking_service")]
        public async Task<IActionResult> CreateBookingService([FromBody] BookingServiceDto bookingDto)
        {
            var userIdClaim = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var book = await _bookingRepository.BookingServiceAsync(bookingDto, userIdClaim);
            return book ? Ok("Success") : BadRequest("Fail");
        }
    }
}
