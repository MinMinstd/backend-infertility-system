﻿using infertility_system.Dtos.MedicalRecord;
using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IMedicalRecordDetailRepository
    {
        Task<ICollection<MedicalRecord>> GetMedicalRecordWithDetailsAsync(int userId);
        Task<MedicalRecordDetail> CreateMedicalRecordDetailAsync(MedicalRecordDetail medicalRecordDetail);
        Task<ICollection<MedicalRecord>> GetMedicalRecordAsync(int userId);
        Task<ICollection<MedicalRecordDetail>> GetMedicalRecordDetailWithTreatmentRoadmapAsync(int userId);
        Task<ICollection<MedicalRecordDetail>> GetMedicalRecordDetailTypetestBaseTreatmentCompleteAsync(int userId);
    }
}
