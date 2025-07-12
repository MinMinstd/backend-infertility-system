namespace infertility_system.Interfaces
{
    using infertility_system.Models;

    public interface IDoctorScheduleRepository
    {
        Task<DoctorSchedule?> GetScheduleByDateTime(DateOnly date, TimeOnly startTime, TimeOnly endTime);

        Task<bool> UpdateScheduleStatus(int scheduleId, string status);

        Task<List<DoctorSchedule>> GetSchedulesByDoctorAndDate(int doctorId, DateOnly date);

        Task<List<DoctorSchedule>> GetScheduleByDoctorId(int doctorId);

        Task<List<DoctorSchedule>> GetListScheduleForDoctorAsync(int userId, DateOnly date);

        Task<List<DoctorSchedule>> GetFullScheduleAsync(int userId);
    }
}