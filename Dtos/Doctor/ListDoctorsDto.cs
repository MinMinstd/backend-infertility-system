namespace infertility_system.Dtos.Doctor
{
    public class ListDoctorsDto
    {
        public int DoctorId { get; set; }
        public string? FullName { get; set; }
        public int Experience { get; set; }
        public string? DegreeName { get; set; }
    }
}
