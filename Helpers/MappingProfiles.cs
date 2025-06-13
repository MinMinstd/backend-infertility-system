using AutoMapper;
using infertility_system.Dtos.Booking;
using infertility_system.Dtos.Customer;
using infertility_system.Dtos.Doctor;
using infertility_system.Dtos.DoctorDegree;
using infertility_system.Dtos.DoctorSchedule;
using infertility_system.Dtos.MedicalRecord;
using infertility_system.Dtos.Service;
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

            CreateMap<BookingDto, Booking>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateOnly.Parse(src.Date)))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => TimeOnly.Parse(src.Time)))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note));

            CreateMap<BookingDto, Order>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateOnly.Parse(src.Date)))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => TimeOnly.Parse(src.Time)))
                .ForMember(dest => dest.Wife, opt => opt.MapFrom(src => src.Wife))
                .ForMember(dest => dest.Husband, opt => opt.MapFrom(src => src.Husband));
            CreateMap<DoctorSchedule, DoctorScheduleDto>();
            CreateMap<MedicalRecordDetailDto, MedicalRecordDetail>();
            CreateMap<Customer, CustomerDto>();
        }
    }
}
