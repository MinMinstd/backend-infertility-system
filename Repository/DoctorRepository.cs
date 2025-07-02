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

        public async Task<List<Customer>> GetListCustomerAsync(int doctorIdClaim)
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

        public async Task<List<MedicalRecordDetail>> GetMedicalRecordDetailAsync(int doctorIdClaim, int customerId)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(x => x.UserId == doctorIdClaim);
            if (doctor == null)
            {
                return null;
            }

            var medicalRecord = await this.context.MedicalRecords
                            .FirstOrDefaultAsync(mr => mr.DoctorId == doctor.DoctorId
                                                    && mr.CustomerId == customerId);

            var medicalRecordDetails = await this.context.MedicalRecordDetails
                            .Where(mrd => mrd.MedicalRecordId == medicalRecord.MedicalRecordId)
                            .Include(tr => tr.TreatmentRoadmap)
                            .ToListAsync();

            return medicalRecordDetails;
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

        public async Task<List<TreatmentRoadmap>> GetTreatmentRoadmapsAsync(int doctorIdClaim, int customerId)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(d => d.UserId == doctorIdClaim);

            var medicalRecord = await this.context.MedicalRecords
                        .FirstOrDefaultAsync(mr => mr.DoctorId == doctor.DoctorId && mr.CustomerId == customerId);

            var medicalRecordDetails = await this.context.MedicalRecordDetails
                        .Where(mrd => mrd.MedicalRecordId == medicalRecord.MedicalRecordId)
                        .ToListAsync();

            var treatmentRoadIds = medicalRecordDetails.Select(mrd => mrd.TreatmentRoadmapId).Distinct().ToList();

            var treatmentRoadmaps = await this.context.TreatmentRoadmaps
                        .Where(tr => treatmentRoadIds.Contains(tr.TreatmentRoadmapId))
                        .ToListAsync();
            return treatmentRoadmaps;
        }

        public async Task<List<Booking>> GetBookingsCustomerAsync(int doctorIdClaim)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(d => d.UserId == doctorIdClaim);

            var doctorSchedules = await this.context.DoctorSchedules
                        .Where(d => d.DoctorId == doctor.DoctorId)
                        .ToListAsync();

            var doctorScheduleId = doctorSchedules.Select(ds => ds.DoctorScheduleId).Distinct().ToList();

            var bookings = await this.context.Bookings
                        .Where(b => doctorScheduleId.Contains((int)b.DoctorScheduleId))
                        .Include(b => b.Customer)
                        .ThenInclude(b => b.Orders)
                        .ThenInclude(b => b.OrderDetails)
                        .ToListAsync();

            return bookings;
        }

        public async Task<Customer> GetPatientInformationAsync(int customerId)
        {
            return await this.context.Customers
                        .Include(c => c.Bookings)
                            .ThenInclude(b => b.Order)
                        .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task<List<TreatmentRoadmap>> GetDetailTreatmentRoadmapAsync(int doctorIdClaim, int customerId)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(d => d.UserId == doctorIdClaim);

            var medicalRecord = await this.context.MedicalRecords
                            .FirstOrDefaultAsync(mr => mr.DoctorId == doctor.DoctorId
                                                    && mr.CustomerId == customerId);

            var medicalRecordDetails = await this.context.MedicalRecordDetails
                            .Where(mrd => mrd.MedicalRecordId == medicalRecord.MedicalRecordId)
                            .ToListAsync();

            var treatmentRoadmapId = medicalRecordDetails.Select(mrd => mrd.TreatmentRoadmapId).Distinct().ToList();

            var treatmentRoadmap = await this.context.TreatmentRoadmaps
                            .Where(tr => treatmentRoadmapId.Contains(tr.TreatmentRoadmapId))
                            .Include(tr => tr.Service)
                            .ToListAsync();

            int step = 1;
            foreach (var tr in treatmentRoadmap)
            {
                tr.TreatmentRoadmapId = step++;
            }

            return treatmentRoadmap;
        }

        public async Task<List<TreatmentResult>> GetTreatmentResultsTypeTestAsync(int doctorIdClaim, int customerId)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(d => d.UserId == doctorIdClaim);

            var medicalRecord = await this.context.MedicalRecords
                            .FirstOrDefaultAsync(mr => mr.DoctorId == doctor.DoctorId
                                                    && mr.CustomerId == customerId);

            var medicalRecordDetails = await this.context.MedicalRecordDetails
                            .Where(mrd => mrd.MedicalRecordId == medicalRecord.MedicalRecordId)
                            .ToListAsync();

            var treatmentResultId = medicalRecordDetails.Select(mrd => mrd.TreatmentResultId).Distinct().ToList();

            var treatmentResult = await this.context.TreatmentResults
                            .Where(tr => treatmentResultId.Contains(tr.TreatmentResultId))
                            .Include(tr => tr.TypeTest)
                            .ToListAsync();

            int step = 1;
            foreach (var item in treatmentResult)
            {
                item.TreatmentRoadmapId = step++;
            }
            return treatmentResult;
        }

        public async Task<List<ConsulationResult>> GetConsultationResultAndTypeTestsAsync(int doctorIdClaim, int customerId)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(d => d.UserId == doctorIdClaim);

            var medicalRecord = await this.context.MedicalRecords
                            .FirstOrDefaultAsync(mr => mr.DoctorId == doctor.DoctorId
                                                    && mr.CustomerId == customerId);

            var medicalRecordDetails = await this.context.MedicalRecordDetails
                            .Where(mrd => mrd.MedicalRecordId == medicalRecord.MedicalRecordId)
                            .ToListAsync();

            var consultationResultId = medicalRecordDetails
                            .Where(mrd => mrd.ConsulationResultId != null)
                            .Select(mrd => mrd.ConsulationResultId).Distinct().ToList();

            var consultationResult = await this.context.ConsulationResults
                            .Where(cr => consultationResultId.Contains(cr.ConsulationResultId))
                            .Include(cr => cr.TypeTests)
                            .ToListAsync();

            return consultationResult;
        }

        public async Task<bool> UpdateDetailTreatmentRoadmapAsync(TreatmentRoadmap updateTreamentRoadmap, string status, 
                                                int treatmentRoadmapId, int customerId)
        {
            var treatmentRoadmap = await this.context.TreatmentRoadmaps.
                                        FirstOrDefaultAsync(tr => tr.TreatmentRoadmapId == treatmentRoadmapId);

            var medicalRecord = await this.context.MedicalRecords.FirstOrDefaultAsync(mr => mr.CustomerId == customerId);

            var medicalRecordDetail = await this.context.MedicalRecordDetails
                                .FirstOrDefaultAsync(mrd => mrd.TreatmentRoadmapId == treatmentRoadmapId
                                                         && mrd.MedicalRecordId == medicalRecord.MedicalRecordId);

            treatmentRoadmap.Date = updateTreamentRoadmap.Date;
            treatmentRoadmap.DurationDay = updateTreamentRoadmap.DurationDay;   
            treatmentRoadmap.Description = updateTreamentRoadmap.Description;

            if (medicalRecordDetail != null && !string.IsNullOrEmpty(status))
            {
                medicalRecordDetail.Status = status;
            }

            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateMedicalRecordDetailAsync(MedicalRecordDetail medicalRecordDetail, int doctorIdClaim, int customerId)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(d => d.UserId == doctorIdClaim);

            var medicalRecord = await this.context.MedicalRecords
                            .Where(mr => mr.DoctorId == doctor.DoctorId
                                      && mr.CustomerId == customerId).FirstOrDefaultAsync();

            medicalRecordDetail.MedicalRecordId = medicalRecord.MedicalRecordId;
            this.context.MedicalRecordDetails.Add(medicalRecordDetail);
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMedicalRecordDetailDtoAsync(MedicalRecordDetail update, int doctorIdClaim, int customerId, int medicalRecordDetailId)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(d => d.UserId == doctorIdClaim);

            var medicalRecord = await this.context.MedicalRecords
                            .FirstOrDefaultAsync(mr => mr.DoctorId == doctor.DoctorId
                                                    && mr.CustomerId == customerId);

            var medicalRecordDetail = await this.context.MedicalRecordDetails
                            .FirstOrDefaultAsync(mrd => mrd.MedicalRecordDetailId == medicalRecordDetailId);

            if(medicalRecordDetail.MedicalRecordId == medicalRecord.MedicalRecordId)
            {
                medicalRecordDetail.Date = update.Date;
                medicalRecordDetail.TestResult = update.TestResult;
                medicalRecordDetail.Note = update.Note;
                medicalRecordDetail.Status = update.Status;
            }
            await this.context.SaveChangesAsync();
            return true;
        }
    }
}
