namespace infertility_system.Dtos.Admin
{
    public class RegisterRequestFromAdminDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public int Experience { get; set; }
        public string? Address { get; set; }
    }
}
