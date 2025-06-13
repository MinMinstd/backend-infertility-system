using AutoMapper;
using infertility_system.Dtos.Booking;
using infertility_system.Dtos.Doctor;
using infertility_system.Dtos.DoctorSchedule;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.AspNetCore.Mvc;

namespace infertility_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IDoctorSchedulesRepository _doctorSchedulesRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository bookingRepository,IDoctorSchedulesRepository doctorSchedulesRepository, IDoctorRepository doctorRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _doctorSchedulesRepository = doctorSchedulesRepository;
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctor()
        {
            var doctors = await _doctorRepository.GetAllDoctorsAsync(null);
            var doctorDto = _mapper.Map<List<DoctorForListDto>>(doctors);
            return Ok(doctorDto);
        }

        [HttpGet("GetListDoctorSchedule/{doctorId}")]
        public async Task<IActionResult> GetAllDoctorScheduleByDoctorId(int doctorId)
        {
            var doctorSchedules = await _doctorSchedulesRepository.GetDoctorScheduleAsync(doctorId);
            var doctorScheduleDtos = _mapper.Map<List<DoctorScheduleDto>>(doctorSchedules);
            return Ok(doctorScheduleDtos);
        }

        [HttpPost("booking_service")]
        public async Task<IActionResult> CreateBookingService([FromBody] BookingDto bookingDto)
        {
            var book = await _bookingRepository.BookingServiceAsync(bookingDto);
            return book ? Ok("Success") : BadRequest("Fail");
        }
    }
}
