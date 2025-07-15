namespace infertility_system.Models
{
    public class DoctorDegree
    {
        public int DoctorDegreeId { get; set; }

        public string? DegreeName { get; set; }

        public int GraduationYear { get; set; }

        public string? Description { get; set; }

        // DoctorDegree N-1 Doctor
        public int DoctorId { get; set; }

        public Doctor? Doctor { get; set; }
    }
}
