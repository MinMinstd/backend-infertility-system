using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IDoctorRepository
    {
        public Task<List<Doctor>> GetAllDoctorsAsync();
        public Task<Doctor?> GetDoctorByIdAsync(int doctorId);
    }
}
