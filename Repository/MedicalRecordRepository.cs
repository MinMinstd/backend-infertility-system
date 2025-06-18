using AutoMapper;
using infertility_system.Data;
using infertility_system.Dtos.MedicalRecord;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MedicalRecordRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<bool> CheckDoctorInBookingAsync(int doctorId)
        {
            return await _context.Bookings.AnyAsync(x => x.DoctorSchedule.DoctorId == doctorId);
        }


        public async Task<bool> CreateMedicalRecordAsync(MedicalRecord medicalRecord, int doctorIdClaim)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.UserId == doctorIdClaim);
            if(doctor == null) return false;

            var doctorValid = await CheckDoctorInBookingAsync(doctor.DoctorId);
            if (!doctorValid) return false;

            var isValid = await CheckCustomerInBookingAsync(medicalRecord.CustomerId);
            if(!isValid) return false;

            medicalRecord.DoctorId = doctor.DoctorId;

            _context.MedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMedicalRecordAsync(MedicalRecord updateRecord, int doctorIdClaims)
        {
            var medicalRecord = await _context.MedicalRecords.
                FirstOrDefaultAsync(x => x.MedicalRecordId == updateRecord.MedicalRecordId);

            if (medicalRecord == null) return false;

            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.UserId == doctorIdClaims);
            var isValid = await CheckDoctorIdInMedicalRecordAsync(doctor.DoctorId, updateRecord.MedicalRecordId);
            if(!isValid) return false;

            medicalRecord.Stage = updateRecord.Stage;
            medicalRecord.Diagnosis = updateRecord.Diagnosis;
            medicalRecord.Status = updateRecord.Status;
            medicalRecord.Attempt = updateRecord.Attempt;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
