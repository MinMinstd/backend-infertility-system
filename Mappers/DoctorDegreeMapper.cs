using infertility_system.Dtos.DoctorDegree;
using infertility_system.Models;

namespace infertility_system.Mappers
{
    public static class DoctorDegreeMapper
    {
        public static DoctorDegreeDto ToDto(this DoctorDegree doctorDegreeModel)
        {
            if (doctorDegreeModel == null)
            {
                return null;
            }
            return new DoctorDegreeDto
            {
                DegreeName = doctorDegreeModel.DegreeName,
                GraduationYear = doctorDegreeModel.GraduationYear,
            };
        }
    }
}
