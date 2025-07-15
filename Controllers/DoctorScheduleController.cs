namespace infertility_system.Controllers
{
    using AutoMapper;
    using infertility_system.Dtos.DoctorSchedule;
    using infertility_system.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

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

        [HttpGet("GetFullScheduleForDoctor")]
        public async Task<IActionResult> GetFullScheduleForDoctor()
        {
            var doctorIdClaims = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var doctorSchedules = await this.doctorScheduleRepository.GetFullScheduleAsync(doctorIdClaims);
            var result = this.mapper.Map<List<DoctorScheduleRespondDto>>(doctorSchedules);
            return this.Ok(result);
        }

        [HttpPost("CreateSchedule")]
        public async Task<IActionResult> CreateSchedule(int doctorId, [FromBody] CreateDoctorScheduleDto createDoctorScheduleDto)
        {
            if (createDoctorScheduleDto == null)
            {
                return this.BadRequest("Invalid schedule data.");
            }

            if (createDoctorScheduleDto.WorkDate < DateOnly.FromDateTime(DateTime.Now))
            {
                return this.BadRequest("Work date cannot be in the past.");
            }

            if (createDoctorScheduleDto.StartTime >= createDoctorScheduleDto.EndTime)
            {
                return this.BadRequest("Start time must be earlier than end time.");
            }

            // Check if the schedule already exists for the given date and time
            var existingSchedule = await this.doctorScheduleRepository.GetScheduleByDateTime(
                createDoctorScheduleDto.WorkDate,
                createDoctorScheduleDto.StartTime,
                createDoctorScheduleDto.EndTime);

            if (existingSchedule != null)
            {
                return this.BadRequest("A schedule already exists for the specified date and time.");
            }

            var doctorSchedule = this.mapper.Map<Models.DoctorSchedule>(createDoctorScheduleDto);
            doctorSchedule.DoctorId = doctorId;
            doctorSchedule.ManagerId = 1;
            doctorSchedule.Status = "Available";

            await this.doctorScheduleRepository.AddScheduleAsync(doctorSchedule);
            return this.Ok("Schedule created successfully.");
        }
    }
}
