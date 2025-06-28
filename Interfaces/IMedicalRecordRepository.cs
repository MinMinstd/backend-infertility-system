namespace infertility_system.Interfaces
{
    using infertility_system.Dtos.MedicalRecord;
    using infertility_system.Models;

    public interface IMedicalRecordRepository
    {
        Task<bool> CreateMedicalRecordAsync(MedicalRecord medicalRecord, int doctorIdClaim);

        Task<bool> UpdateMedicalRecordAsync(MedicalRecord updateRecord, int doctorIdClaims);

        Task<bool> CheckDoctorIdInMedicalRecordAsync(int doctorId, int medicalRecordId);

        Task<bool> CheckCustomerInBookingAsync(int customerId);

        Task<bool> CheckDoctorInBookingAsync(int doctorId);
    }
}
