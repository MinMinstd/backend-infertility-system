namespace infertility_system.Interfaces
{
    using infertility_system.Dtos.MedicalRecord;
    using infertility_system.Models;

    public interface IMedicalRecordDetailRepository
    {

        Task<ICollection<MedicalRecord>> GetMedicalRecordAsync(int userId);

        Task<ICollection<MedicalRecordDetail>> GetMedicalRecordDetailWithTreatmentResultAndTypetestAsync(int medicalRecordId);
    }
}
