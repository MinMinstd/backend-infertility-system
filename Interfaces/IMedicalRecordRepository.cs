using infertility_system.Dtos.MedicalRecord;
using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IMedicalRecordRepository
    {
        Task<bool> CreateMedicalRecordAsync(CreateMedicalRecordDto dto, int doctorIdClaim, int customerId);
        Task<bool> UpdateMedicalRecordAsync(UpdateMedicalRecordDto dto, int medicalRecordId, int doctorIdClaims);
        Task<bool> CheckDoctorIdInMedicalRecordAsync(int doctorId, int medicalRecordId);
        Task<bool> CheckCustomerInBookingAsync(int customerId);
        Task<bool> CheckDoctorInBookingAsync(int doctorId);
    }
}
