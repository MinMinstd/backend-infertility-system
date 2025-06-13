using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IMedicalRecordDetailRepository
    {
        Task<MedicalRecordDetail> CreateMedicalRecordDetailAsync(MedicalRecordDetail medicalRecordDetail);
    }
}
