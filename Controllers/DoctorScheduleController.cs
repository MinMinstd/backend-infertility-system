using infertility_system.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace infertility_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorScheduleController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DoctorScheduleController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetDoctorSchedule()
        {
            // Lấy userId từ token
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Tìm doctorId tương ứng
            int doctorId = await _context.Doctors
                .Where(d => d.UserId == userId)
                .Select(d => d.DoctorId)
                .FirstOrDefaultAsync();

            if (doctorId == 0)
                return NotFound("Doctor not found");

            // Truy vấn dữ liệu
            var schedule = await _context.DoctorSchedules
                .Where(ds => ds.DoctorId == doctorId)
                .Include(ds => ds.Bookings) // Thêm dòng này để tải dữ liệu quan hệ
                .Select(ds => new
                {
                    ds.WorkDate,
                    ds.StartTime,
                    ds.EndTime,
                    ds.Status,
                    Bookings = ds.Bookings.Select(b => new
                    {
                        b.BookingId,
                        b.Type,
                        b.Status,
                        b.Customer.FullName
                    }).ToList()
                })
                .ToListAsync();

            return Ok(schedule);
        }
    }
}
