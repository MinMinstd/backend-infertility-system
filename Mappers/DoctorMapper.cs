using infertility_system.Dtos.Doctor;
using infertility_system.Models;

namespace infertility_system.Mappers
{
    public static class DoctorMapper
    {
        public static DoctorForListDto ToDtoForList(this Doctor doctorModel)
        {
            if(doctorModel == null)
            {
                return null;
            }
            return new DoctorForListDto
            {
                DoctorId = doctorModel.DoctorId,
                FullName = doctorModel.FullName,
                Experience = doctorModel.Experience,
            };
        }

        public static DoctorDto ToDto(this Doctor doctorModel)
        {
            if(doctorModel == null)
            {
                return null;
            }
            return new DoctorDto
            {
                FullName = doctorModel.FullName,
                Email = doctorModel.Email,
                Phone = doctorModel.Phone,
                Experience = doctorModel.Experience,
                DoctorDegrees = doctorModel.DoctorDegrees?.Select(d => d.ToDto()).ToList()
            };
        }
    }
}
