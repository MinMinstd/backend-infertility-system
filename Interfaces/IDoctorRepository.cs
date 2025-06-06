using infertility_system.Helpers;
using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IDoctorRepository
    {
        public Task<List<Doctor>> GetAllDoctorsAsync(QueryDoctor query);
        public Task<Doctor?> GetDoctorByIdAsync(int doctorId);
    }
}
