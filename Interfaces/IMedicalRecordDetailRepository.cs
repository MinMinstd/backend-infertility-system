using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IMedicalRecordDetailRepository
    {
        Task<ICollection<MedicalRecordDetail>> GetMedicalRecordsDetailAsync(int userId);
        Task<MedicalRecordDetail> CreateMedicalRecordDetailAsync(MedicalRecordDetail medicalRecordDetail);
    }
}
