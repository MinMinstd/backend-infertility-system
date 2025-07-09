namespace infertility_system.Helpers
{
    using AutoMapper;
    using infertility_system.Dtos.Booking;
    using infertility_system.Dtos.ConsulationResult;
    using infertility_system.Dtos.Customer;
    using infertility_system.Dtos.Doctor;
    using infertility_system.Dtos.DoctorDegree;
    using infertility_system.Dtos.DoctorSchedule;
    using infertility_system.Dtos.Embryo;
    using infertility_system.Dtos.Feedback;
    using infertility_system.Dtos.MedicalRecord;
    using infertility_system.Dtos.MedicalRecordDetail;

    using infertility_system.Dtos.OrderDetailDto;

    using infertility_system.Dtos.Order;
    using infertility_system.Dtos.OrderDetail;
    using infertility_system.Dtos.Payment;

    using infertility_system.Dtos.Service;
    using infertility_system.Dtos.TreatmentResult;
    using infertility_system.Dtos.TreatmentRoadmap;
    using infertility_system.Dtos.Typetests;
    using infertility_system.Dtos.TypeTests;
    using infertility_system.Dtos.User;
    using infertility_system.Models;

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            this.CreateMap<Embryo, EmbryoDto>();
            this.CreateMap<CreateMedicalRecordDto, MedicalRecord>();
            this.CreateMap<UpdateMedicalRecordDto, MedicalRecord>();
            this.CreateMap<MedicalRecord, UpdateMedicalRecordDto>();
            this.CreateMap<Doctor, DoctorDto>();
            this.CreateMap<Doctor, DoctorForListDto>();
            this.CreateMap<DoctorDegree, DoctorDegreeDto>();
            this.CreateMap<ServiceDB, ServiceToDtoForList>();
            this.CreateMap<BookingServiceDto, Booking>();
            this.CreateMap<BookingConsulantDto, Booking>();
            this.CreateMap<DoctorSchedule, DoctorScheduleToBookingDto>();
            this.CreateMap<Customer, CustomerDto>();
            this.CreateMap<Feedback, FeedbackResponseDto>();
            this.CreateMap<ConsulationResultRequest, ConsulationResult>();

            // CreateMap<ConsulationRegistration, ConsulationRegistrationRespond>();
            this.CreateMap<FeedbackRequestDto, Feedback>();
            this.CreateMap<User, UserRespondDto>();
            this.CreateMap<User, UserToManagementDto>()
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => src.Role == "Customer" ? src.Customer.FullName : src.Doctor.FullName));
            this.CreateMap<Doctor, DoctorForManagementDto>()
            .ForMember(
                dest => dest.DegreeName,
                opt => opt.MapFrom(src => src.DoctorDegrees.FirstOrDefault().DegreeName))
            .ForMember(
                dest => dest.DoctorId,
                opt => opt.MapFrom(src => src.DoctorId));
            this.CreateMap<DoctorSchedule, DoctorScheduleRespondDto>();

            this.CreateMap<ServiceDB, ServiceToBookingDto>();

            // CreateMap<Doctor, DoctorBookingRespondDto>();
            this.CreateMap<Customer, ListCustomerInDoctorDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.MedicalRecord.FirstOrDefault().Status))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.MedicalRecord.FirstOrDefault().StartDate))
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.MedicalRecord.FirstOrDefault().Doctor.ServiceDB.Name))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.Birthday)));

            this.CreateMap<Doctor, DoctorBookingServiceRespondDto>();
            this.CreateMap<Doctor, DoctorBookingConsulationRespondDto>();
            this.CreateMap<Doctor, ListDoctorsDto>()
                .ForMember(dest => dest.DegreeName, opt => opt.MapFrom(src => src.DoctorDegrees.FirstOrDefault().DegreeName));
            this.CreateMap<Doctor, DoctorDetailDto>()
                .ForMember(dest => dest.DegreeName, opt => opt.MapFrom(src => src.DoctorDegrees.FirstOrDefault().DegreeName))
                .ForMember(dest => dest.GraduationYear, opt => opt.MapFrom(src => src.DoctorDegrees.FirstOrDefault().GraduationYear));

            this.CreateMap<MedicalRecordDetail, MedicalRecordDetailDto>()
                .ForMember(dest => dest.StepNumber, opt => opt.MapFrom(src => src.TreatmentRoadmapId))
                .ForMember(dest => dest.Stage, opt => opt.MapFrom(src => src.TreatmentRoadmap.Stage));
            this.CreateMap<MedicalRecord, MedicalRecordDto>();

            this.CreateMap<TypeTest, TypeTestDto>();
            this.CreateMap<MedicalRecordDetail, MedicalRecordDetailWithTreatmentResultAndTypeTestDto>()
                .ForMember(dest => dest.Stage, opt => opt.MapFrom(src => src.TreatmentResult.Stage))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.TreatmentResult.Description))
                .ForMember(dest => dest.TypeTest, opt => opt.MapFrom(src => src.TreatmentResult.TypeTest))
                .ForMember(dest => dest.DurationDay, opt => opt.MapFrom(src => src.TreatmentResult.DurationDay));

            this.CreateMap<Customer, PatientInformationDto>()
                .ForMember(dest => dest.Wife, opt => opt.MapFrom(src => src.Bookings.FirstOrDefault().Order.Wife))
                .ForMember(dest => dest.Husband, opt => opt.MapFrom(src => src.Bookings.FirstOrDefault().Order.Husband))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.Birthday)));

            this.CreateMap<MedicalRecord, UseServiceByCustomerDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Doctor.FullName))
                .ForMember(dest => dest.NameService, opt => opt.MapFrom(src => src.Doctor.ServiceDB.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Doctor.ServiceDB.Description));

            this.CreateMap<TreatmentRoadmap, TreatmentRoadmapDetailDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.MedicalRecordDetails.FirstOrDefault().Status));

            this.CreateMap<TreatmentResult, TreatmentResultDto>()
                .ForMember(dest => dest.StepNumber, opt => opt.MapFrom(src => src.TreatmentRoadmapId))
                .ForMember(dest => dest.TypeTest, opt => opt.MapFrom(src => src.TypeTest))
                .ForMember(dest => dest.DateTreatmentResult, opt => opt.MapFrom(src => src.DateTreatmentResult));

            this.CreateMap<ConsulationResult, ConsultationResultDto>()
                .ForMember(dest => dest.TypeTests, opt => opt.MapFrom(src => src.TypeTests));

            this.CreateMap<UpdateDetailTreatmentRoadmapDto, TreatmentRoadmap>();
            this.CreateMap<CreateMedicalRecordDetailDto, MedicalRecordDetail>();
            this.CreateMap<UpdateMedicalRecordDetailDto, MedicalRecordDetail>();
            this.CreateMap<CreateTreatmentResultAndTypeTestDto, TreatmentResult>();
            this.CreateMap<CreateTypeTestDto, TypeTest>();
            this.CreateMap<CreateConsultatioResultAndTypeTestDto, ConsulationResult>();
            this.CreateMap<CreateBookingCustomerDto, Booking>();
            this.CreateMap<OrderDetail, ListAppointmentDto>();

            this.CreateMap<CustomerProfileDto, Customer>();
            this.CreateMap<CustomerProfileDto, User>();
            this.CreateMap<Booking, BookingForListDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.DoctorSchedule.Doctor.FullName));

            this.CreateMap<TreatmentRoadmap, TreatmentRoadmapDto>();
            this.CreateMap<Booking, BookingCustomerDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Customer.FullName))
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Order.OrderDetails.FirstOrDefault().ServiceName));

            this.CreateMap<Booking, BookingInCustomerDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.DoctorSchedule.Doctor.FullName))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DoctorSchedule.Doctor.ServiceDB.Name));

            this.CreateMap<Order, OrderToPaymentDto>()
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().ServiceName))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.Customer.Birthday)))
                .ForMember(dest => dest.Wife, opt => opt.MapFrom(src => src.Wife))
                .ForMember(dest => dest.Husband, opt => opt.MapFrom(src => src.Husband))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Customer.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Customer.Email))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Customer.Address))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Customer.Birthday))
                .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().ServiceId));
            this.CreateMap<Order, OrderDto>();
            this.CreateMap<OrderDetail, OrderDetailDto>();

            this.CreateMap<TreatmentRoadmap, TreatmentRoadmapToPaymentDto>();
            this.CreateMap<TreatmentRoadmap, ListTreatmentRoadMapDto>()
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name));

            this.CreateMap<Payment, HistoryPaymentDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Order.Customer.FullName))
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.TreatmentRoadmap.Service.Name));
            this.CreateMap<Payment, PaymentDetailDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Order.Customer.FullName))
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.TreatmentRoadmap.Service.Name))
                .ForMember(dest => dest.Stage, opt => opt.MapFrom(src => src.TreatmentRoadmap.Stage))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.TreatmentRoadmap.Price));
        }

        private static int CalculateAge(DateOnly birthday)
        {
            var birthDate = birthday.ToDateTime(TimeOnly.MinValue);
            var today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            return age;
        }
    }
}
