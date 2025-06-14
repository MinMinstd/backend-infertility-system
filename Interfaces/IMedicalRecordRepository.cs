using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IMedicalRecordRepository
    {
        Task<MedicalRecord> CreateMedicalRecordAsync(MedicalRecord medicalRecord);
        Task<MedicalRecord> UpdateMedicalRecordAsync(int medicalRecordId, MedicalRecord medicalRecord);
        Task<bool> CheckDoctorIdInMedicalRecordAsync(int doctorId, int medicalRecordId);
    }
}
