using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetDoctorsAsync();
        //Task<Doctor> GetDoctorByIdAsync(int id);
        //Task<Doctor> CreateDoctorAsync(Doctor doctor);
        //Task<Doctor> UpdateDoctorAsync(int id, Doctor doctor);
        //Task<bool> DeleteDoctorAsync(int id);
        //Task<IEnumerable<Doctor>> GetDoctorsBySpecialtyAsync(string specialty);
        //Task<IEnumerable<Doctor>> GetDoctorsByLocationAsync(string location);
    }
}
