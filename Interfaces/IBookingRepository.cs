using infertility_system.Dtos.Booking;
using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IBookingRepository
    {

        Task<bool> CheckCustomerInBookingAsync(int customerId);
        Task<bool> BookingServiceAsync(BookingServiceDto dto, int userId);
        Task<bool> BookingConsulantAsync(BookingConsulantDto dto, int userId);
        Task<List<DoctorSchedule>> GetDoctorScheduleAsync(int doctorId, DateOnly date);
        Task<List<Doctor>> GetAllDoctorAsync();

    }
}
