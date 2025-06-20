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
using infertility_system.Dtos.Typetests;
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


            CreateMap<ServiceDB, ServiceToBookingDto>();
            CreateMap<Doctor, DoctorBookingServiceRespondDto>();
            CreateMap<Doctor, DoctorBookingConsulationRespondDto>();


            CreateMap<MedicalRecord, MedicalRecordWithDetailDto>();
            CreateMap<MedicalRecordDetail, MedicalRecordDetailDto>();
            CreateMap<MedicalRecord, MedicalRecordDto>();
            CreateMap<MedicalRecordDetail, MedicalRecordDetailWithTreatmentDto>()
                .ForMember(dest => dest.Stage, opt => opt.MapFrom(src => src.TreatmentRoadmap.Stage));

            CreateMap<TypeTest, TypeTestDto>();
            CreateMap<MedicalRecordDetail, MedicalRecordDetailWithTypeTestDto>()
                .ForMember(dest => dest.TypeTest, opt => opt.MapFrom(src => src.TreatmentResult.TypeTest));

            CreateMap<CustomerProfileDto, Customer>();
            CreateMap<CustomerProfileDto, User>();
            CreateMap<Booking, BookingForListDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.DoctorSchedule.Doctor.FullName));
            
        }
    }
}
