using infertility_system.Helpers;
using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetListDoctorsAsync(QueryDoctor? query);
        Task<List<Doctor>> GetAllDoctorsAsync();
        Task<Doctor?> GetDoctorByIdAsync(int doctorId);

        Task<List<Doctor>> GetDoctorsByServiceIdAsync(int serviceId);
        Task<List<Customer>> GetListCustomerFullInforAsync(int doctorIdClaim);
        Task<List<MedicalRecord>> GetMedicalRecordWithDetailAsync(int doctorIdClaim, int customerId);
        Task<List<MedicalRecordDetail>> GetMedicalRecordDetailWithTreatmentResultAndTypeTestAsync(int doctorIdClaim, int customerId);
        Task<List<TreatmentRoadmap>> GetTreatmentRoadmapsAsync(int doctorIdClaim, int customerId);
        Task<List<Booking>> GetBookingsCustomerAsync(int doctorIdClaim);

        Task<List<Doctor>> GetDoctosForManagement();
        Task<List<Doctor>> GetDoctorsByServiceIdForBookingService(int serviceId);
        Task<List<Doctor>> GetDoctorsByServiceIdForBookingConsulation(int serviceId);

    }
}
