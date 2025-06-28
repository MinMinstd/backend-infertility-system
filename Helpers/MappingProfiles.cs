namespace infertility_system.Helpers
{
    using AutoMapper;
    using infertility_system.Dtos.Booking;
    using infertility_system.Dtos.ConsulationResult;
    using infertility_system.Dtos.Customer;
    using infertility_system.Dtos.Doctor;
    using infertility_system.Dtos.DoctorDegree;
    using infertility_system.Dtos.DoctorSchedule;
    using infertility_system.Dtos.Feedback;
    using infertility_system.Dtos.MedicalRecord;
    using infertility_system.Dtos.Service;
    using infertility_system.Dtos.TreatmentRoadmap;
    using infertility_system.Dtos.Typetests;
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
            this.CreateMap<Customer, CustomerInDoctorDto>()
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

            this.CreateMap<MedicalRecord, MedicalRecordWithDetailDto>();
            this.CreateMap<MedicalRecordDetail, MedicalRecordDetailDto>()
                .ForMember(dest => dest.Stage, opt => opt.MapFrom(src => src.TreatmentRoadmap.Stage));
            this.CreateMap<MedicalRecord, MedicalRecordDto>();

            this.CreateMap<TypeTest, TypeTestDto>();
            this.CreateMap<MedicalRecordDetail, MedicalRecordDetailWithTypeTestDto>()
                .ForMember(dest => dest.TypeTest, opt => opt.MapFrom(src => src.TreatmentResult.TypeTest));

            this.CreateMap<CustomerProfileDto, Customer>();
            this.CreateMap<CustomerProfileDto, User>();
            this.CreateMap<Booking, BookingForListDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.DoctorSchedule.Doctor.FullName));

            this.CreateMap<TreatmentRoadmap, TreatmentRoadmapDto>();
            this.CreateMap<Booking, BookingCustomerDto>();

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
