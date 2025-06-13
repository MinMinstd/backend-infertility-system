using infertility_system.Dtos.Booking;
using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IBookingRepository
    {
        Task<bool> BookingServiceAsync(BookingDto dto);
        Task<List<DoctorSchedule>> GetDoctorScheduleAsync(int doctorId);
        Task<List<Doctor>> GetAllDoctorAsync();
    }
}
