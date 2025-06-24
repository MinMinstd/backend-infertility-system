using AutoMapper;
using infertility_system.Dtos.Booking;
using infertility_system.Dtos.Email;
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
        private readonly IDoctorScheduleRepository _doctorScheduleRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository bookingRepository, IDoctorScheduleRepository doctorScheduleRepository, IDoctorRepository doctorRepository, IMapper mapper, IEmailService emailService)
        {
            _bookingRepository = bookingRepository;
            _doctorScheduleRepository = doctorScheduleRepository;
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        [HttpPost("booking_consulant")]
        public async Task<IActionResult> CreateBookingConsultant([FromBody] BookingConsulantDto bookingDto)
        {
            var userIdClaim = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var book = await _bookingRepository.BookingConsulantAsync(bookingDto, userIdClaim);
            if (book)
            {
                var emailMessage = new EmailMessage
                (
                    new List<String> { userEmail },
                    "Thông báo đặt lịch tư vấn",
                    "<p>Bạn đã đặt lịch tư vấn ngày <strong>" + bookingDto.Date + "</strong> lúc <strong>" + bookingDto.Time + "</strong>.</p>"
                );

                await _emailService.SendEmail(emailMessage);

                return Ok("Success");
            }
            return BadRequest("Fail");
        }

        [HttpPost("booking_service")]
        public async Task<IActionResult> CreateBookingService([FromBody] BookingServiceDto bookingDto)
        {
            var userIdClaim = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var book = await _bookingRepository.BookingServiceAsync(bookingDto, userIdClaim);

            if (book)
            {
                var emailMessage = new EmailMessage
                (
                    new List<String> { userEmail },
                    "Thông báo đặt lịch khám",
                    "<p>Bạn đã đặt lịch khám ngày <strong>" + bookingDto.Date + "</strong> lúc <strong>" + bookingDto.Time + "</strong>.</p>"
                );

                await _emailService.SendEmail(emailMessage);

                return Ok("Success");
            }
            return BadRequest("Fail");
        }

        [HttpGet("GetListBooking")]
        public async Task<IActionResult> GetListBooking()
        {
            var bookings = await _bookingRepository.GetListBooking();

            return Ok(_mapper.Map<List<BookingForListDto>>(bookings));
        }
    }
}
