using infertility_system.Data;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly AppDbContext _context;
        public MedicalRecordRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckDoctorIdInMedicalRecordAsync(int doctorId, int medicalRecordId)
        {
            var medical = await _context.MedicalRecords.FirstOrDefaultAsync(x => x.MedicalRecordId == medicalRecordId);
            if (medical == null)
                return false;
            return medical.DoctorId == doctorId;
        }

        public async Task<MedicalRecord> CreateMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            await _context.MedicalRecords.AddAsync(medicalRecord);
            await _context.SaveChangesAsync();
            return medicalRecord;
        }

        public async Task<MedicalRecord> UpdateMedicalRecordAsync(int medicalRecordId, MedicalRecord medicalRecord)
        {
            var medicalRecordExists = await _context.MedicalRecords.
                FirstOrDefaultAsync(x => x.MedicalRecordId == medicalRecordId
                && x.CustomerId == medicalRecord.CustomerId);

            if (medicalRecordExists == null)
                return null;

            medicalRecordExists.Stage = medicalRecord.Stage;
            medicalRecordExists.Diagnosis = medicalRecord.Diagnosis;
            medicalRecordExists.Status = medicalRecord.Status;

            await _context.SaveChangesAsync();
            return medicalRecordExists;
        }
    }
}
