namespace infertility_system.Dtos.Doctor
{
    public class DoctorForManagementDto
    {
        public int DoctorId { get; set; }

        public string? FullName { get; set; }

        public string? DegreeName { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public bool? IsActive { get; set; }
    }
}
