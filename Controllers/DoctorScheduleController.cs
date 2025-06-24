using AutoMapper;
using infertility_system.Dtos.DoctorSchedule;
using infertility_system.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace infertility_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorScheduleController : ControllerBase
    {
        private readonly IDoctorScheduleRepository _doctorScheduleRepository;
        private readonly IMapper _mapper;
        public DoctorScheduleController(IDoctorScheduleRepository doctorScheduleRepository, IMapper mapper)
        {
            _doctorScheduleRepository = doctorScheduleRepository;
            _mapper = mapper;
        }
        //[HttpGet]
        //public async Task<IActionResult> GetDoctorSchedule()
        //{
        //    // Lấy userId từ token
        //    int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

        //    // Tìm doctorId tương ứng
        //    int doctorId = await _context.Doctors
        //        .Where(d => d.UserId == userId)
        //        .Select(d => d.DoctorId)
        //        .FirstOrDefaultAsync();

        //    if (doctorId == 0)
        //        return NotFound("Doctor not found");

        //    // Truy vấn dữ liệu
        //    var schedule = await _context.DoctorSchedules
        //        .Where(ds => ds.DoctorId == doctorId)
        //        .Include(ds => ds.Bookings) // Thêm dòng này để tải dữ liệu quan hệ
        //        .Select(ds => new
        //        {
        //            ds.WorkDate,
        //            ds.StartTime,
        //            ds.EndTime,
        //            ds.Status,
        //            Bookings = ds.Bookings.Select(b => new
        //            {
        //                b.BookingId,
        //                b.Type,
        //                b.Status,
        //                b.Customer.FullName
        //            }).ToList()
        //        })
        //        .ToListAsync();

        //    return Ok(schedule);
        //}

        [HttpGet("GetListDoctorSchedule/{doctorId}")]
        public async Task<IActionResult> GetAllDoctorScheduleByDoctorId(int doctorId, [FromQuery] DateOnly date)
        {
            var doctorSchedules = await _doctorScheduleRepository.GetSchedulesByDoctorAndDate(doctorId, date);

            var doctorScheduleDtos = _mapper.Map<List<DoctorScheduleToBookingDto>>(doctorSchedules);
            return Ok(doctorScheduleDtos);
        }

        [HttpGet("GetScheduleByDoctorId/{doctorId}")]
        public async Task<IActionResult> GetScheduleByDoctorId(int doctorId)
        {
            var doctorSchedule = await _doctorScheduleRepository.GetScheduleByDoctorId(doctorId);
            var doctorScheduleDto = _mapper.Map<List<DoctorScheduleRespondDto>>(doctorSchedule);
            return Ok(doctorScheduleDto);
        }
    }
}
