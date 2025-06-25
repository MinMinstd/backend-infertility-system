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

namespace infertility_system.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Embryo, EmbryoDto>();
            CreateMap<CreateMedicalRecordDto, MedicalRecord>();
            CreateMap<UpdateMedicalRecordDto, MedicalRecord>();
            CreateMap<MedicalRecord, UpdateMedicalRecordDto>();
            CreateMap<Doctor, DoctorDto>();
            CreateMap<Doctor, DoctorForListDto>();
            CreateMap<DoctorDegree, DoctorDegreeDto>();
            CreateMap<ServiceDB, ServiceToDtoForList>();
            CreateMap<BookingServiceDto, Booking>();
            CreateMap<BookingConsulantDto, Booking>();
            CreateMap<DoctorSchedule, DoctorScheduleToBookingDto>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<Feedback, FeedbackResponseDto>();
            CreateMap<ConsulationResultRequest, ConsulationResult>();
            //CreateMap<ConsulationRegistration, ConsulationRegistrationRespond>();
            CreateMap<FeedbackRequestDto, Feedback>();
            CreateMap<User, UserRespondDto>();
            CreateMap<User, UserToManagementDto>()
                .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => src.Role == "Customer" ? src.Customer.FullName : src.Doctor.FullName));
            CreateMap<Doctor, DoctorForManagementDto>()
            .ForMember(dest => dest.DegreeName,
                       opt => opt.MapFrom(src => src.DoctorDegrees.FirstOrDefault().DegreeName))
            .ForMember(dest => dest.DoctorId,
                       opt => opt.MapFrom(src => src.DoctorId));
            CreateMap<DoctorSchedule, DoctorScheduleRespondDto>();

            CreateMap<ServiceDB, ServiceToBookingDto>();

            //CreateMap<Doctor, DoctorBookingRespondDto>();
            CreateMap<Customer, CustomerInDoctorDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.MedicalRecord.FirstOrDefault().Status))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.MedicalRecord.FirstOrDefault().StartDate))
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.MedicalRecord.FirstOrDefault().Doctor.ServiceDB.Name))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.Birthday)));

            CreateMap<Doctor, DoctorBookingServiceRespondDto>();
            CreateMap<Doctor, DoctorBookingConsulationRespondDto>();
            CreateMap<Doctor, ListDoctorsDto>()
                .ForMember(dest => dest.DegreeName, opt => opt.MapFrom(src => src.DoctorDegrees.FirstOrDefault().DegreeName));
            CreateMap<Doctor, DoctorDetailDto>()
                .ForMember(dest => dest.DegreeName, opt => opt.MapFrom(src => src.DoctorDegrees.FirstOrDefault().DegreeName))
                .ForMember(dest => dest.GraduationYear, opt => opt.MapFrom(src => src.DoctorDegrees.FirstOrDefault().GraduationYear));

            CreateMap<MedicalRecord, MedicalRecordWithDetailDto>();
            CreateMap<MedicalRecordDetail, MedicalRecordDetailDto>()
                .ForMember(dest => dest.Stage, opt => opt.MapFrom(src => src.TreatmentRoadmap.Stage));
            CreateMap<MedicalRecord, MedicalRecordDto>();
            

            CreateMap<TypeTest, TypeTestDto>();
            CreateMap<MedicalRecordDetail, MedicalRecordDetailWithTypeTestDto>()
                .ForMember(dest => dest.TypeTest, opt => opt.MapFrom(src => src.TreatmentResult.TypeTest));

            CreateMap<CustomerProfileDto, Customer>();
            CreateMap<CustomerProfileDto, User>();
            CreateMap<Booking, BookingForListDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.DoctorSchedule.Doctor.FullName));

            CreateMap<TreatmentRoadmap, TreatmentRoadmapDto>();
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
