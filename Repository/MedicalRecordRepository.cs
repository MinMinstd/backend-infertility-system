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

        public async Task<bool> CreateMedicalRecordAsync(CreateMedicalRecordDto dto, int doctorIdClaim, int customerId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.UserId == doctorIdClaim);
            if(doctor == null) return false;

            var doctorValid = await CheckDoctorInBookingAsync(doctor.DoctorId);
            if (!doctorValid) return false;

            var isValid = await CheckCustomerInBookingAsync(customerId);
            if(!isValid) return false;

            var medicalRecord = _mapper.Map<MedicalRecord>(dto);
            medicalRecord.CustomerId = customerId;
            medicalRecord.DoctorId = doctor.DoctorId;
            medicalRecord.StartDate = dto.StartDate;
            medicalRecord.EndDate = dto.EndDate;
            medicalRecord.Stage = dto.Stage;
            medicalRecord.Diagnosis = dto.Diagnosis;
            medicalRecord.Status = dto.Status;
            medicalRecord.Attempt = dto.Attempt;

            _context.MedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMedicalRecordAsync(UpdateMedicalRecordDto dto, 
                            int medicalRecordId, int doctorIdClaims)
        {
            var medicalRecord = await _context.MedicalRecords.
                FirstOrDefaultAsync(x => x.MedicalRecordId == medicalRecordId);

            if (medicalRecord == null) return false;

            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.UserId == doctorIdClaims);
            var isValid = await CheckDoctorIdInMedicalRecordAsync(doctor.DoctorId, medicalRecordId);
            if(!isValid) return false;

            medicalRecord.Stage = dto.Stage;
            medicalRecord.Diagnosis = dto.Diagnosis;
            medicalRecord.Status = dto.Status;
            medicalRecord.Attempt = dto.Attempt;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
