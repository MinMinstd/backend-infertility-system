namespace infertility_system.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int Experience { get; set; }

        // Doctor 1-N DoctorDegree
        public List<DoctorDegree>? Degrees { get; set; }

        // Doctor 1-N DoctorSchedule
        public List<DoctorSchedule>? DoctorSchedules { get; set; }

        // Doctor 1-N MedicalRecord
        public List<MedicalRecord>? MedicalRecords { get; set; }
    }
}
