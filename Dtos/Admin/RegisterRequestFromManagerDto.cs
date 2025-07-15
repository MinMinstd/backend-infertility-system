namespace infertility_system.Dtos.Admin
{
    public class RegisterRequestFromManagernDto
    {
        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Password { get; set; }

        public int Experience { get; set; }

        public int ServiceDBId { get; set; }

        public string? DegreeName { get; set; }

        public int GraduationYear { get; set; }
    }
}
