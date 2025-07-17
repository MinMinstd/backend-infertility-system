namespace infertility_system.Repository
{
    using System.Net;
    using infertility_system.Data;
    using infertility_system.Interfaces;
    using infertility_system.Middleware;
    using infertility_system.Models;
    using Microsoft.EntityFrameworkCore;

    public class DoctorScheduleRepository : IDoctorScheduleRepository
    {
        private readonly AppDbContext context;

        public DoctorScheduleRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<DoctorSchedule?> GetScheduleByDateTime(DateOnly date, TimeOnly startTime, TimeOnly endTime)
        {
            return await this.context.DoctorSchedules
                .FirstOrDefaultAsync(x => x.StartTime == startTime &&
                                        x.EndTime == endTime &&
                                        x.WorkDate == date);
        }

        public async Task<bool> UpdateScheduleStatus(int scheduleId, string status)
        {
            var schedule = await this.context.DoctorSchedules.FindAsync(scheduleId);
            if (schedule == null)
            {
                return false;
            }

            schedule.Status = status;
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<List<DoctorSchedule>> GetSchedulesByDoctorAndDate(int doctorId, DateOnly date)
        {
            return await this.context.DoctorSchedules
                .Where(x => x.DoctorId == doctorId && x.WorkDate == date && x.Status == "Available")
                .ToListAsync();
        }

        public async Task<List<DoctorSchedule>> GetScheduleByDoctorId(int doctorId)
        {
            return await this.context.DoctorSchedules
                .Where(x => x.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<List<DoctorSchedule>> GetListScheduleForDoctorAsync(int userId, DateOnly date)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(d => d.UserId == userId);
            if (doctor == null)
            {
                throw new CustomHttpException(HttpStatusCode.NotFound, "Doctor not found.");
            }

            var doctorSchedules = await this.context.DoctorSchedules
                        .Where(ds => ds.DoctorId == doctor.DoctorId && ds.Status == "Available" && ds.WorkDate == date)
                        .ToListAsync();
            return doctorSchedules;
        }

        public async Task<List<DoctorSchedule>> GetFullScheduleAsync(int userId)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(d => d.UserId == userId);

            var doctorSchedules = await this.context.DoctorSchedules
                        .Where(ds => ds.DoctorId == doctor.DoctorId)
                        .ToListAsync();
            return doctorSchedules;
        }

        public async Task AddScheduleAsync(DoctorSchedule schedule)
        {
            await this.context.DoctorSchedules.AddAsync(schedule);
            await this.context.SaveChangesAsync();
        }
    }
}