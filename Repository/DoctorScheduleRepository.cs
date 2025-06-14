using infertility_system.Data;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class DoctorScheduleRepository : IDoctorScheduleRepository
    {
        private readonly AppDbContext _context;

        public DoctorScheduleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DoctorSchedule?> GetScheduleByDateTime(DateOnly date, TimeOnly startTime, TimeOnly endTime)
        {
            return await _context.DoctorSchedules
                .FirstOrDefaultAsync(x => x.StartTime == startTime && 
                                        x.EndTime == endTime && 
                                        x.WorkDate == date);
        }

        public async Task<bool> UpdateScheduleStatus(int scheduleId, string status)
        {
            var schedule = await _context.DoctorSchedules.FindAsync(scheduleId);
            if (schedule == null) return false;

            schedule.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<DoctorSchedule>> GetSchedulesByDoctorAndDate(int doctorId, DateOnly date)
        {
            return await _context.DoctorSchedules
                .Where(x => x.DoctorId == doctorId && x.WorkDate == date && x.Status == "Available")
                .ToListAsync();
        }
    }
} 