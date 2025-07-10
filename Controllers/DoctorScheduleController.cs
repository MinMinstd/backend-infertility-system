namespace infertility_system.Controllers
{
    using System.Security.Claims;
    using AutoMapper;
    using infertility_system.Dtos.DoctorSchedule;
    using infertility_system.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class DoctorScheduleController : ControllerBase
    {
        private readonly IDoctorScheduleRepository doctorScheduleRepository;
        private readonly IMapper mapper;

        public DoctorScheduleController(IDoctorScheduleRepository doctorScheduleRepository, IMapper mapper)
        {
            this.doctorScheduleRepository = doctorScheduleRepository;
            this.mapper = mapper;
        }

        // [HttpGet]
        // public async Task<IActionResult> GetDoctorSchedule()
        // {
        //    // Lấy userId từ token
        //    int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

        // // Tìm doctorId tương ứng
        //    int doctorId = await _context.Doctors
        //        .Where(d => d.UserId == userId)
        //        .Select(d => d.DoctorId)
        //        .FirstOrDefaultAsync();

        // if (doctorId == 0)
        //        return NotFound("Doctor not found");

        // // Truy vấn dữ liệu
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

        // return Ok(schedule);
        // }
        [HttpGet("GetListDoctorSchedule/{doctorId}")]
        public async Task<IActionResult> GetAllDoctorScheduleByDoctorId(int doctorId, [FromQuery] DateOnly date)
        {
            var doctorSchedules = await this.doctorScheduleRepository.GetSchedulesByDoctorAndDate(doctorId, date);

            var doctorScheduleDtos = this.mapper.Map<List<DoctorScheduleToBookingDto>>(doctorSchedules);
            return this.Ok(doctorScheduleDtos);
        }

        [HttpGet("GetScheduleByDoctorId/{doctorId}")]
        public async Task<IActionResult> GetScheduleByDoctorId(int doctorId)
        {
            var doctorSchedule = await this.doctorScheduleRepository.GetScheduleByDoctorId(doctorId);
            var doctorScheduleDto = this.mapper.Map<List<DoctorScheduleRespondDto>>(doctorSchedule);
            return this.Ok(doctorScheduleDto);
        }

        [HttpGet("GetListScheduleForDoctor")]
        public async Task<IActionResult> GetListScheduleForDoctor([FromQuery] DateOnly date)
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var doctorSchedules = await this.doctorScheduleRepository.GetListScheduleForDoctorAsync(doctorIdClaims, date);
            var result = this.mapper.Map<List<DoctorScheduleRespondDto>>(doctorSchedules);
            return this.Ok(result);
        }
    }
}
