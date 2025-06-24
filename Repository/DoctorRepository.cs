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
        public async Task<List<Doctor>> GetListDoctorsAsync(QueryDoctor? query)
        {
            var doctors = _context.Doctors.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.FullName))
            {
                doctors = doctors.Where(x => x.FullName.Contains(query.FullName));
            }

            return await doctors.ToListAsync();
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
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

        public async Task<List<Doctor>> GetDoctorsByServiceIdForBookingService(int serviceId)
        {
            return await _context.Doctors
                .Where(x => x.ServiceDBId == serviceId)
                .ToListAsync();
        }

        public async Task<List<Customer>> GetListCustomerFullInforAsync(int doctorIdClaim)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.UserId == doctorIdClaim);

            var doctorSchedule = await _context.DoctorSchedules.FirstOrDefaultAsync(x => x.DoctorId == doctor.DoctorId);
            if (doctorSchedule == null) return null;

            var bookings = await _context.Bookings
                    .Where(x => x.DoctorScheduleId == doctorSchedule.DoctorScheduleId)
                    .ToListAsync();

            var customerIds = bookings.Select(x => x.CustomerId).Distinct().ToList();

            var customers = await _context.Customers
                    .Where(x => customerIds.Contains(x.CustomerId))
                    .Include(x => x.MedicalRecord)
                    .ThenInclude(m => m.Doctor)
                    .ThenInclude(d => d.ServiceDB)
                    .ToListAsync();
            return customers;
        }

        public async Task<List<MedicalRecord>> GetMedicalRecordWithDetailAsync(int doctorIdClaim, int customerId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.UserId == doctorIdClaim);
            if (doctor == null) return null;

            var medicalRecords = await _context.MedicalRecords
                        .Where(m => m.DoctorId == doctor.DoctorId && m.CustomerId == customerId)
                        .Include(m => m.MedicalRecordDetails)
                        .ToListAsync();

            return medicalRecords;
        }

        public async Task<List<Doctor>> GetDoctorsByServiceIdForBookingConsulation(int serviceId)
        {
            return await _context.Doctors
                .Where(x => x.ServiceDBId == serviceId)
                .ToListAsync();
        }

        public async Task<List<Doctor>> GetDoctosForManagement()
        {
            return await _context.Doctors.Include(x => x.DoctorDegrees).ToListAsync();

        }
    }
}
