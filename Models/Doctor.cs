namespace infertility_system.Models
{
    public class Doctor
    {
        public int UserId { get; set; }
        public int DoctorId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int Experience { get; set; }

        // Doctor 1-N DoctorDegree
        public List<DoctorDegree>? DoctorDegrees { get; set; }

        // Doctor 1-N DoctorSchedule
        public List<DoctorSchedule>? DoctorSchedules { get; set; }

        // Doctor 1-N MedicalRecord
        public List<MedicalRecord>? MedicalRecords { get; set; }

        //Service N-1 Doctor
        public int ServiceDBId { get; set; }
        public ServiceDB? ServiceDB { get; set; }
    }
}
