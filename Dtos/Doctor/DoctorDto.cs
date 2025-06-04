using infertility_system.Dtos.DoctorDegree;
using infertility_system.Models;

namespace infertility_system.Dtos.Doctor
{
    public class DoctorDto
    {
        public String? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int Experience { get; set; }
        public List<DoctorDegreeDto> DoctorDegrees { get; set; }
    }
}
