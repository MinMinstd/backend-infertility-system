using AutoMapper;
using infertility_system.Dtos.MedicalRecord;
using infertility_system.Models;

namespace infertility_system.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<MedicalRecordDetail, MedicalRecordDetailDto>()
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.TreatmentRoadmap.Service.Name))
                .ForMember(dest => dest.Stage, opt => opt.MapFrom(src => src.TreatmentRoadmap.Stage))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.MedicalRecord.Doctor.FullName));
            CreateMap<Embryo, EmbryoDto>();
        }
    }
}
