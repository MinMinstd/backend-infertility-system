using infertility_system.Helpers;
using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetListDoctorsAsync(QueryDoctor? query);
        Task<List<Doctor>> GetAllDoctorsAsync();
        Task<Doctor?> GetDoctorByIdAsync(int doctorId);
        Task<List<Doctor>> GetDoctosForManagement();
        Task<List<Doctor>> GetDoctorsByServiceIdForBookingService(int serviceId);
        Task<List<Doctor>> GetDoctorsByServiceIdForBookingConsulation(int serviceId);
    }
}
