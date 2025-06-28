namespace infertility_system.Repository
{
    using infertility_system.Data;
    using infertility_system.Helpers;
    using infertility_system.Interfaces;
    using infertility_system.Models;
    using Microsoft.EntityFrameworkCore;

    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext context;

        public DoctorRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Doctor>> GetListDoctorsAsync(QueryDoctor? query)
        {
            var doctors = this.context.Doctors.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.FullName))
            {
                doctors = doctors.Where(x => x.FullName.Contains(query.FullName));
            }

            return await doctors.ToListAsync();
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
            return await this.context.Doctors.ToListAsync();
        }

        public async Task<Doctor?> GetDoctorByIdAsync(int doctorId)
        {
            var doctorModel = await this.context.Doctors.Include(x => x.DoctorDegrees)
                .FirstOrDefaultAsync(x => x.DoctorId == doctorId);
            if (doctorModel == null)
            {
                return null;
            }

            return doctorModel;
        }

        public async Task<List<Doctor>> GetDoctorsByServiceIdForBookingService(int serviceId)
        {
            return await this.context.Doctors
                .Where(x => x.ServiceDBId == serviceId)
                .ToListAsync();
        }

        public async Task<List<Customer>> GetListCustomerFullInforAsync(int doctorIdClaim)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(x => x.UserId == doctorIdClaim);

            var doctorSchedule = await this.context.DoctorSchedules.FirstOrDefaultAsync(x => x.DoctorId == doctor.DoctorId);
            if (doctorSchedule == null)
            {
                return null;
            }

            var bookings = await this.context.Bookings
                    .Where(x => x.DoctorScheduleId == doctorSchedule.DoctorScheduleId)
                    .ToListAsync();

            var customerIds = bookings.Select(x => x.CustomerId).Distinct().ToList();

            var customers = await this.context.Customers
                    .Where(x => customerIds.Contains(x.CustomerId))
                    .Include(x => x.MedicalRecord)
                    .ThenInclude(m => m.Doctor)
                    .ThenInclude(d => d.ServiceDB)
                    .ToListAsync();
            return customers;
        }

        public async Task<List<MedicalRecord>> GetMedicalRecordWithDetailAsync(int doctorIdClaim, int customerId)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(x => x.UserId == doctorIdClaim);
            if (doctor == null)
            {
                return null;
            }

            var medicalRecords = await this.context.MedicalRecords
                        .Where(m => m.DoctorId == doctor.DoctorId && m.CustomerId == customerId)
                        .Include(m => m.MedicalRecordDetails)
                        .ThenInclude(mrd => mrd.TreatmentRoadmap)
                        .ToListAsync();

            return medicalRecords;
        }

        public async Task<List<Doctor>> GetDoctorsByServiceIdForBookingConsulation(int serviceId)
        {
            return await this.context.Doctors
                .Where(x => x.ServiceDBId == serviceId)
                .ToListAsync();
        }

        public async Task<List<Doctor>> GetDoctosForManagement()
        {
            return await this.context.Doctors.Include(x => x.DoctorDegrees).ToListAsync();
        }

        public Task<List<Doctor>> GetDoctorsByServiceIdAsync(int serviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MedicalRecordDetail>> GetMedicalRecordDetailWithTreatmentResultAndTypeTestAsync(int doctorIdClaim, int customerId)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(d => d.UserId == doctorIdClaim);

            if (doctor == null)
            {
                return null;
            }

            var medicalRecords = await this.context.MedicalRecords
                        .Where(mr => mr.DoctorId == doctor.DoctorId && mr.CustomerId == customerId)
                        .ToListAsync();
            if (medicalRecords == null)
            {
                return null;
            }

            var medicalRecordDetails = await this.context.MedicalRecordDetails
                        .Where(mrd => mrd.MedicalRecordId == medicalRecords.FirstOrDefault().MedicalRecordId)
                        .Include(mrd => mrd.TreatmentResult)
                        .ThenInclude(tr => tr.TypeTest)
                        .ToListAsync();
            return medicalRecordDetails;
        }

        public async Task<List<TreatmentRoadmap>> GetTreatmentRoadmapsAsync(int doctorIdClaim, int customerId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == doctorIdClaim);

            var medicalRecord = await _context.MedicalRecords
                        .FirstOrDefaultAsync(mr => mr.DoctorId == doctor.DoctorId && mr.CustomerId == customerId);

            var medicalRecordDetails = await _context.MedicalRecordDetails
                        .Where(mrd => mrd.MedicalRecordId == medicalRecord.MedicalRecordId)
                        .ToListAsync();

            var treatmentRoadIds = medicalRecordDetails.Select(mrd => mrd.TreatmentRoadmapId).Distinct().ToList();

            var treatmentRoadmaps = await _context.TreatmentRoadmaps
                        .Where(tr => treatmentRoadIds.Contains(tr.TreatmentRoadmapId))
                        .ToListAsync();
            return treatmentRoadmaps;
        }

        public async Task<List<Booking>> GetBookingsCustomerAsync(int doctorIdClaim)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == doctorIdClaim);

            var doctorSchedules = await _context.DoctorSchedules
                            .Where(ds => ds.DoctorId == doctor.DoctorId)
                            .ToListAsync();

            var doctorScheduleId = doctorSchedules.Select(ds => ds.DoctorScheduleId).Distinct().ToList();

            var bookings = await _context.Bookings
                            .Where(b => doctorScheduleId.Contains((int)b.DoctorScheduleId))
                            .ToListAsync();
            return bookings;
        }
    }
}
