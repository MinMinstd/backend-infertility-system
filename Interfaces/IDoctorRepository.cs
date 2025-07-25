﻿namespace infertility_system.Interfaces
{
    using infertility_system.Dtos.Booking;
    using infertility_system.Dtos.ConsulationResult;
    using infertility_system.Dtos.Customer;
    using infertility_system.Dtos.MedicalRecord;
    using infertility_system.Dtos.TreatmentResult;
    using infertility_system.Helpers;
    using infertility_system.Models;

    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetListDoctorsAsync(QueryDoctor? query);

        Task<List<Doctor>> GetAllDoctorsAsync();

        Task<Doctor?> GetDoctorByIdAsync(int doctorId);

        Task<List<Doctor>> GetDoctorsByServiceIdAsync(int serviceId);

        Task<List<ListCustomerInDoctorDto>> GetListCustomerAsync(int doctorIdClaim);

        Task<Customer> GetPatientInformationAsync(int customerId);

        Task<List<MedicalRecordDetail>> GetMedicalRecordDetailAsync(int doctorIdClaim, int medicalRecordId);

        Task<List<TreatmentRoadmap>> GetDetailTreatmentRoadmapAsync(int doctorIdClaim, int customerId);

        Task<List<TreatmentResult>> GetTreatmentResultsTypeTestAsync(int bookingId, int customerId);

        Task<List<ConsulationResult>> GetConsultationResultAndTypeTestsAsync(int bookingId, int customerId);

        Task<List<TreatmentRoadmap>> GetTreatmentRoadmapsAsync(int bookingId, int customerId);

        Task<List<Booking>> GetBookingsCustomerAsync(int customerId);

        Task<List<Doctor>> GetDoctosForManagement();

        Task<List<Doctor>> GetDoctorsByServiceIdForBookingService(int serviceId);

        Task<List<Doctor>> GetDoctorsByServiceIdForBookingConsulation(int serviceId);

        Task<List<MedicalRecordWithBookingDto>> GetMedicalRecordsCustomerAsync(int customerId, int doctorIdClaim);

        Task<List<OrderDetail>> GetListAppointmentCustomerAsync(int bookingId);

        Task<bool> CreateMedicalRecordDetailAsync(MedicalRecordDetail medicalRecordDetail, int doctorIdClaim, int medicalRecordId);

        Task<bool> CreateTreatmentResultAndTypeTestAsync(CreateTreatmentResultAndTypeTestDto dto, int doctorIdClaim, int customerId);

        Task<bool> CreateTypeTestTreatementResultAsync(TypeTest create, int doctorIdClaim, int customerId, int treatmentResultId);

        Task<bool> CreateConsultationAndTypeTestAsync(CreateConsultatioResultAndTypeTestDto dto, int bookingId, int customerId);

        Task<bool> CreateTypeTestConsultationResultAsync(TypeTest create, int doctorIdClaim, int customerId, int consultationResultId);

        Task<bool> CreateBookingForCustomerAsync(CreateBookingCustomerDto dto, int bookingId);

        Task<bool> CreatePaymentForCustomerAsync(int bookingId, int treamentRoadmapId);

        Task<bool> UpdateDetailTreatmentRoadmapAsync(TreatmentRoadmap updateTreamentRoadmap, string status, int treatmentRoadmapId, int customerId);

        Task<bool> UpdateMedicalRecordDetailAsync(MedicalRecordDetail update, int doctorIdClaim, int customerId, int medicalRecordDetailId);

        Task<bool> UpdateTreatmentResultAndTypeTestAsync(UpdateTreatmentResultAndTypetestDto dto, int treatmentResultId);

        Task<bool> UpdateConsultationResultAndTypeTestAsync(UpdateConsultationResultAndTypetestDto dto, int consultationResultId);

        Task<bool> UpdateStatusBookingAfterCompleteAsync(int bookingId, string status);

        Task<List<ListCustomerInDoctorDto>> FindCustomerByNameAsync(string name, int doctorIdClaim);

        Task<int> AmountCustomerAsync(int doctorIdClaim);

        Task<int> AmountMedicalRecordAsync(int doctorIdClaim);

        Task<int> AmountMedicalRecordWithStatusCompleteAsync(int doctorIdClaim);

        Task<int> AmountBookingCustomerAsync(int doctorIdClaim);

        Task<List<MedicalRecord>> GetListMedicalRecordWithStartDateAsync(int doctorIdClaim);

        Task<List<MedicalRecord>> GetMedicalRecordsCompleteAsync(int doctorIdClaim);

        Task<List<MedicalRecord>> GetMedicalRecordsInProgressAsync(int doctorIdClaim);
    }
}
