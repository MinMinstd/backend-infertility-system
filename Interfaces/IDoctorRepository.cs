using infertility_system.Helpers;
using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IDoctorRepository
    {
        public Task<List<Doctor>> GetAllDoctorsAsync(QueryDoctor query);
        public Task<Doctor?> GetDoctorByIdAsync(int doctorId);
        public Task<MedicalRecord> CreateMedicalRecordAsync(MedicalRecord medicalRecord);
        public Task<MedicalRecord> UpdateMedicalRecordAsync(int customerId, MedicalRecord medicalRecord);
        public Task<MedicalRecordDetail> CreateMedicalRecordDetailAsync(MedicalRecordDetail medicalRecordDetail);
        public Task<bool> CheckCustomerInBookingAsync(int customerId);
        public Task<bool> CheckDoctorIdInMedicalRecordAsync(int doctorId, int medicalRecordId);
    }
}
