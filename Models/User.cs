namespace infertility_system.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; } // e.g., "Patient", "Doctor", "Manager"
    }
}
