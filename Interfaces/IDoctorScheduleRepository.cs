using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IDoctorScheduleRepository
    {
        Task<DoctorSchedule?> GetScheduleByDateTime(DateOnly date, TimeOnly startTime, TimeOnly endTime);
        Task<bool> UpdateScheduleStatus(int scheduleId, string status);
        Task<List<DoctorSchedule>> GetSchedulesByDoctorAndDate(int doctorId, DateOnly date);
    }
} 