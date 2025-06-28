namespace infertility_system.Controllers
{
    using System.Security.Claims;
    using AutoMapper;
    using infertility_system.Dtos.Booking;
    using infertility_system.Dtos.Email;
    using infertility_system.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository bookingRepository;
        private readonly IDoctorScheduleRepository doctorScheduleRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly IEmailService emailService;
        private readonly IMapper mapper;

        public BookingController(IBookingRepository bookingRepository, IDoctorScheduleRepository doctorScheduleRepository, IDoctorRepository doctorRepository, IMapper mapper, IEmailService emailService)
        {
            this.bookingRepository = bookingRepository;
            this.doctorScheduleRepository = doctorScheduleRepository;
            this.doctorRepository = doctorRepository;
            this.mapper = mapper;
            this.emailService = emailService;
        }

        [HttpPost("booking_consulant")]
        public async Task<IActionResult> CreateBookingConsultant([FromBody] BookingConsulantDto bookingDto)
        {
            var userIdClaim = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userEmail = this.User.FindFirst(ClaimTypes.Email)?.Value;
            var book = await this.bookingRepository.BookingConsulantAsync(bookingDto, userIdClaim);
            if (book)
            {
                var emailMessage = new EmailMessage(
                    new List<string> { userEmail },
                    "Thông báo đặt lịch tư vấn",
                    "<p>Bạn đã đặt lịch tư vấn ngày <strong>" + bookingDto.Date + "</strong> lúc <strong>" + bookingDto.Time + "</strong>.</p>");

                await this.emailService.SendEmail(emailMessage);

                return this.Ok("Success");
            }

            return this.BadRequest("Fail");
        }

        [HttpPost("booking_service")]
        public async Task<IActionResult> CreateBookingService([FromBody] BookingServiceDto bookingDto)
        {
            var userIdClaim = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userEmail = this.User.FindFirst(ClaimTypes.Email)?.Value;
            var book = await this.bookingRepository.BookingServiceAsync(bookingDto, userIdClaim);

            if (book)
            {
                var emailMessage = new EmailMessage(
                    new List<string> { userEmail },
                    "Thông báo đặt lịch khám",
                    "<p>Bạn đã đặt lịch khám ngày <strong>" + bookingDto.Date + "</strong> lúc <strong>" + bookingDto.Time + "</strong>.</p>");

                await this.emailService.SendEmail(emailMessage);

                return this.Ok("Success");
            }

            return this.BadRequest("Fail");
        }

        [HttpGet("GetListBooking")]
        public async Task<IActionResult> GetListBooking()
        {
            var bookings = await this.bookingRepository.GetListBooking();

            return this.Ok(this.mapper.Map<List<BookingForListDto>>(bookings));
        }
    }
}
