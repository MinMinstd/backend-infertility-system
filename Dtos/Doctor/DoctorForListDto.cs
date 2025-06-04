using infertility_system.Dtos.DoctorDegree;

namespace infertility_system.Dtos.Doctor
{
    public class DoctorForListDto
    {
        public String? FullName { get; set; }
        public int Experience { get; set; }
        public List<DoctorDegreeDto> DoctorDegrees { get; set; }
    }
}
