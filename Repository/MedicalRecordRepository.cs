namespace infertility_system.Repository
{
    using System.Net;
    using AutoMapper;
    using infertility_system.Data;
    using infertility_system.Interfaces;
    using infertility_system.Middleware;
    using infertility_system.Models;
    using Microsoft.EntityFrameworkCore;

    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public MedicalRecordRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> CheckCustomerInBookingAsync(int customerId)
        {
            return await this.context.Bookings.AnyAsync(x => x.CustomerId == customerId);
        }

        public async Task<bool> CheckDoctorIdInMedicalRecordAsync(int doctorId, int medicalRecordId)
        {
            var medical = await this.context.MedicalRecords.FirstOrDefaultAsync(x => x.MedicalRecordId == medicalRecordId);
            if (medical == null)
            {
                return false;
            }

            return medical.DoctorId == doctorId;
        }

        public async Task<bool> CheckDoctorInBookingAsync(int doctorId)
        {
            return await this.context.Bookings.AnyAsync(x => x.DoctorSchedule.DoctorId == doctorId);
        }

        //tạo medicalRecord cho customer
        public async Task<bool> CreateMedicalRecordAsync(MedicalRecord medicalRecord, int doctorIdClaim)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(x => x.UserId == doctorIdClaim);
            if (doctor == null)
            {
                return false;
            }

            var doctorValid = await this.CheckDoctorInBookingAsync(doctor.DoctorId);
            if (!doctorValid)
            {
                return false;
            }

            var isValid = await this.CheckCustomerInBookingAsync(medicalRecord.CustomerId);
            if (!isValid)
            {
                return false;
            }

            medicalRecord.DoctorId = doctor.DoctorId;

            this.context.MedicalRecords.Add(medicalRecord);
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMedicalRecordAsync(MedicalRecord updateRecord, int doctorIdClaims)
        {
            var medicalRecord = await this.context.MedicalRecords.
                FirstOrDefaultAsync(x => x.MedicalRecordId == updateRecord.MedicalRecordId);

            if (medicalRecord == null)
            {
                return false;
            }

            var doctor = await this.context.Doctors.FirstOrDefaultAsync(x => x.UserId == doctorIdClaims);
            var isValid = await this.CheckDoctorIdInMedicalRecordAsync(doctor.DoctorId, updateRecord.MedicalRecordId);
            if (!isValid)
            {
                return false;
            }

            var startDate = medicalRecord.StartDate;
            var endDate = updateRecord.EndDate;
            if (startDate < endDate)
            {
                medicalRecord.EndDate = updateRecord.EndDate;
            }
            else
            {
                throw new CustomHttpException(HttpStatusCode.BadRequest, "Ngày kết thúc phải sau ngày bắt đầu.");
            }
            medicalRecord.Stage = updateRecord.Stage;
            medicalRecord.Diagnosis = updateRecord.Diagnosis;
            medicalRecord.Status = updateRecord.Status;
            medicalRecord.Attempt = updateRecord.Attempt;

            await this.context.SaveChangesAsync();
            return true;
        }
    }
}
