using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IDoctorSchedulesRepository
    {
        Task<List<DoctorSchedule>> GetDoctorScheduleAsync(int doctorId);
    }
}
