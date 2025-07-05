namespace infertility_system.Repository
{
    using AutoMapper;
    using infertility_system.Data;
    using infertility_system.Dtos.Booking;
    using infertility_system.Dtos.ConsulationResult;
    using infertility_system.Dtos.MedicalRecord;
    using infertility_system.Dtos.TreatmentResult;
    using infertility_system.Helpers;
    using infertility_system.Interfaces;
    using infertility_system.Models;
    using Microsoft.EntityFrameworkCore;

    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly IDoctorScheduleRepository doctorScheduleRepository;

        public DoctorRepository(AppDbContext context, IMapper mapper, IDoctorScheduleRepository doctorScheduleRepository)
        {
            this.context = context;
            this.mapper = mapper;
            this.doctorScheduleRepository = doctorScheduleRepository;
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

        public async Task<List<MedicalRecordDetail>> GetMedicalRecordDetailAsync(int doctorIdClaim, int medicalRecordId)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(x => x.UserId == doctorIdClaim);
            if (doctor == null)
            {
                return null;
            }

            var medicalRecord = await this.context.MedicalRecords
                            .FirstOrDefaultAsync(mr => mr.DoctorId == doctor.DoctorId
                                                    && mr.MedicalRecordId == medicalRecordId);

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

        public async Task<List<TreatmentRoadmap>> GetDetailTreatmentRoadmapAsync(int bookingId, int customerId)
        {
            var booking = await this.context.Bookings
                        .FirstOrDefaultAsync(b => b.BookingId == bookingId
                                               && b.CustomerId == customerId);

            var order = await this.context.Orders.FirstOrDefaultAsync(o => o.BookingId == booking.BookingId);

            var orderDetail = await this.context.OrderDetails.FirstOrDefaultAsync(od => od.OrderId == order.OrderId);

            var service = await this.context.Services.FirstOrDefaultAsync(s => s.ServiceDBId == orderDetail.ServiceId);

            var treatmentRoadmap = await this.context.TreatmentRoadmaps
                        .Where(tr => tr.ServiceId == service.ServiceDBId)
                        .Include(tr => tr.MedicalRecordDetails)
                        .ToListAsync();

            foreach (var tr in treatmentRoadmap)
            {

                if (tr.MedicalRecordDetails == null || !tr.MedicalRecordDetails.Any())
                {
                    tr.MedicalRecordDetails = new List<MedicalRecordDetail>
                    {
                        new MedicalRecordDetail { Status = "Chưa thực hiện" }
                    };
                }
            }

            return treatmentRoadmap;
        }

        public async Task<List<TreatmentResult>> GetTreatmentResultsTypeTestAsync(int bookingId, int customerId)
        {
            var booking = await this.context.Bookings
                        .FirstOrDefaultAsync(b => b.BookingId == bookingId
                                               && b.CustomerId == customerId);

            var order = await this.context.Orders.FirstOrDefaultAsync(o => o.BookingId == booking.BookingId);

            var orderDetail = await this.context.OrderDetails.FirstOrDefaultAsync(od => od.OrderId == order.OrderId);

            var service = await this.context.Services.FirstOrDefaultAsync(s => s.ServiceDBId == orderDetail.ServiceId);

            var treatmentRoadmap = await this.context.TreatmentRoadmaps
                        .Where(tr => tr.ServiceId == service.ServiceDBId)
                        .ToListAsync();

            var treatmentRoadmapId = treatmentRoadmap.Select(tr => tr.TreatmentRoadmapId).Distinct().ToList();

            var treatmentResult = await this.context.TreatmentResults
                            .Where(tr => treatmentRoadmapId.Contains(tr.TreatmentRoadmapId))
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

        public async Task<bool> CreateMedicalRecordDetailAsync(MedicalRecordDetail medicalRecordDetail, int doctorIdClaim, int medicalRecordId)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(d => d.UserId == doctorIdClaim);

            var medicalRecord = await this.context.MedicalRecords
                            .Where(mr => mr.DoctorId == doctor.DoctorId
                                      && mr.MedicalRecordId == medicalRecordId).FirstOrDefaultAsync();

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

        public async Task<bool> CreateTreatmentResultAndTypeTestAsync(CreateTreatmentResultAndTypeTestDto dto, int doctorIdClaim, int customerId)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(d => d.UserId == doctorIdClaim);

            var medicalRecord = await this.context.MedicalRecords
                            .FirstOrDefaultAsync(mr => mr.DoctorId == doctor.DoctorId
                                                    && mr.CustomerId == customerId);

            if(medicalRecord == null) return false;

            var treatmentResult = this.mapper.Map<TreatmentResult>(dto);
            treatmentResult.DateTreatmentResult = dto.DateTreatmentResult;
            treatmentResult.Stage = dto.Stage;
            treatmentResult.Description = dto.Description;
            treatmentResult.DurationDay = dto.DurationDay;
            treatmentResult.Result = dto.Result;

            var typeTest = new TypeTest()
            {
                Name = dto.Name,
                Description = dto.DescriptionTypeTest,
                TreatmentResult = treatmentResult,
            };
            this.context.TreatmentResults.Add(treatmentResult);
            this.context.TypeTests.Add(typeTest);
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateTypeTestTreatementResultAsync(TypeTest create, int doctorIdClaim, int customerId, int treatmentResultId)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(d => d.UserId == doctorIdClaim);

            var medicalRecord = await this.context.MedicalRecords
                        .FirstOrDefaultAsync(mr => mr.DoctorId == doctor.DoctorId
                                                && mr.CustomerId == customerId);
            if(medicalRecord == null)
            {
                Console.WriteLine("Lỗi");
                return false;
            }

            var typeTest = new TypeTest
            {
                Name = create.Name,
                Description = create.Description,
                TreatmentResultId = treatmentResultId,
            };
            this.context.TypeTests.Add(typeTest);
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTreatmentResultAndTypeTestAsync(UpdateTreatmentResultAndTypetestDto dto, int treatmentResultId)
        {
            var treatmentResult = await this.context.TreatmentResults
                        .Include(tr => tr.TypeTest)
                        .FirstOrDefaultAsync(tr => tr.TreatmentResultId == treatmentResultId);

            if (treatmentResult == null) return false;

            treatmentResult.DateTreatmentResult = dto.DateTreatmentResult;
            treatmentResult.Description = dto.Description;
            treatmentResult.Result = dto.Result;


            var listTypeTest = treatmentResult.TypeTest.ToList();

            foreach (var typeTestDto in dto.TypeTest)
            {
                if (typeTestDto.TypeTestId == 0)
                {
                    treatmentResult.TypeTest.Add(new TypeTest
                    {
                        Name = typeTestDto.Name,
                        Description = typeTestDto.Description,
                    });
                }
                else
                {
                    var existing = listTypeTest.FirstOrDefault(t => t.TypeTestId == typeTestDto.TypeTestId);
                    if (existing != null)
                    {
                        existing.Name = typeTestDto.Name;
                        existing.Description = typeTestDto.Description;
                    }
                }
            }

            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateConsultationAndTypeTestAsync(CreateConsultatioResultAndTypeTestDto dto, int doctorIdClaim, int customerId)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(d => d.UserId == doctorIdClaim);

            var doctorSchedule = await this.context.DoctorSchedules
                        .Where(ds => ds.DoctorId == doctor.DoctorId)
                        .ToListAsync();

            var doctorScheduleId = doctorSchedule.Select(ds => ds.DoctorScheduleId).Distinct().ToList();

            var booking = await this.context.Bookings
                    .Where(b => doctorScheduleId.Contains((int)b.DoctorScheduleId) 
                             && b.CustomerId == customerId).ToListAsync();

            var consulationResult = this.mapper.Map<ConsulationResult>(dto);
            consulationResult.BookingId = booking.FirstOrDefault().BookingId;
            consulationResult.Date = dto.Date;
            consulationResult.ResultValue = dto.ResultValue;
            consulationResult.Note = dto.Note;

            var typeTest = new TypeTest()
            {
                Name = dto.Name,
                Description = dto.DescriptionTypeTest,
                ConsulationResult = consulationResult,
            };

            this.context.ConsulationResults.Add(consulationResult);
            this.context.TypeTests.Add(typeTest);
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateTypeTestConsultationResultAsync(TypeTest create, int doctorIdClaim, int customerId, int consultationResultId)
        {
            var doctor = await this.context.Doctors.FirstOrDefaultAsync(d => d.UserId == doctorIdClaim);

            var doctorSchedule = await this.context.DoctorSchedules
                        .Where(ds => ds.DoctorId == doctor.DoctorId)
                        .ToListAsync();

            var doctorScheduleId = doctorSchedule.Select(ds => ds.DoctorScheduleId).Distinct().ToList();

            var booking = await this.context.Bookings
                    .Where(b => doctorScheduleId.Contains((int)b.DoctorScheduleId)
                             && b.CustomerId == customerId).ToListAsync();

            if(booking == null) return false;

            var typeTest = new TypeTest
            {
                Name = create.Name,
                Description = create.Description,
                ConsulationResultId = consultationResultId,
            };
            this.context.TypeTests.Add(typeTest);
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateConsultationResultAndTypeTestAsync(UpdateConsultationResultAndTypetestDto dto, int consultationResultId)
        {
            var consultationResult = await this.context.ConsulationResults
                        .Include(tr => tr.TypeTests)
                        .Where(cs => cs.ConsulationResultId == consultationResultId)
                        .FirstOrDefaultAsync();

            consultationResult.Date = dto.Date;
            consultationResult.ResultValue = dto.ResultValue;
            consultationResult.Note = dto.Note;

            var listTypeTest = consultationResult.TypeTests.ToList();

            foreach (var typeTestDto in dto.TypeTest)
            {
                if (typeTestDto.TypeTestId == 0)
                {
                    consultationResult.TypeTests.Add(new TypeTest
                    {
                        Name = typeTestDto.Name,
                        Description = typeTestDto.Description,
                    });
                }
                else
                {
                    var existing = listTypeTest.FirstOrDefault(t => t.TypeTestId == typeTestDto.TypeTestId);
                    if (existing != null)
                    {
                        existing.Name = typeTestDto.Name;
                        existing.Description = typeTestDto.Description;
                    }
                }
            }

            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MedicalRecordWithBookingDto>> GetMedicalRecordsCustomerAsync(int customerId)
        {
            var bookings = await this.context.Bookings
                        .Where(b => b.CustomerId == customerId)
                        .ToListAsync();

            var medicalRecords = await this.context.MedicalRecords
                        .Where(mr => mr.CustomerId == customerId)
                        .ToListAsync();

            var result = new List<MedicalRecordWithBookingDto>();

            for (int i = 0; i < medicalRecords.Count; i++)
            {
                var record = medicalRecords[i];
                var booking = bookings.ElementAtOrDefault(i); // ánh xạ theo index hoặc logic khác

                result.Add(new MedicalRecordWithBookingDto
                {
                    BookingId = booking.BookingId,
                    MedicalRecordId = record.MedicalRecordId,
                    StartDate = record.StartDate,
                    EndDate = record.EndDate,
                    Stage = record.Stage,
                    Diagnosis = record.Diagnosis,
                    Status = record.Status,
                    Attempt = record.Attempt
                });
            }

            return result;
        }

        public async Task<bool> CreateBookingForCustomerAsync(CreateBookingCustomerDto dto, int doctorIdClaim, int customerId)
        {
            var customer = await this.context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);

            var doctor = await this.context.Doctors.FirstOrDefaultAsync(d => d.UserId == doctorIdClaim);

            var medicalRecord = await this.context.MedicalRecords
                        .FirstOrDefaultAsync(mr => mr.DoctorId == doctor.DoctorId
                                                && mr.CustomerId == customer.CustomerId);
            if (medicalRecord == null) return false;

            var booking = this.mapper.Map<Booking>(dto);
            booking.Status = "Pending";
            booking.CustomerId = customer.CustomerId;
            booking.Type = "Service";

            await this.doctorScheduleRepository.UpdateScheduleStatus(dto.DoctorScheduleId, "Unavailable");

            this.context.Bookings.Add(booking);
            await this.context.SaveChangesAsync();
            return true;
        }
    }
}
