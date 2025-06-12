using infertility_system.Data;
using infertility_system.Helpers;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;
        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckCustomerInBookingAsync(int customerId)
        {
            return await _context.Bookings.AnyAsync(x => x.CustomerId == customerId);
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

        public async Task<MedicalRecordDetail> CreateMedicalRecordDetailAsync(MedicalRecordDetail medicalRecordDetail)
        {
            await _context.MedicalRecordDetails.AddAsync(medicalRecordDetail);
            await _context.SaveChangesAsync();
            return medicalRecordDetail;
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync(QueryDoctor query)
        {
            var doctors = _context.Doctors.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.FullName))
            {
                doctors = doctors.Where(x => x.FullName.Contains(query.FullName));
            }

            return await doctors.ToListAsync();
        }

        public async Task<Doctor?> GetDoctorByIdAsync(int doctorId)
        {
            var doctorModel = await _context.Doctors.Include(x => x.DoctorDegrees)
                .FirstOrDefaultAsync(x => x.DoctorId == doctorId);
            if (doctorModel == null)
            {
                return null;
            }
            return doctorModel;
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
