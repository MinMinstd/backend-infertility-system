namespace infertility_system.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public byte[]? PasswordHash { get; set; } // Optional, for hashed passwords
        public byte[]? PasswordSalt { get; set; } // Optional, for hashed passwords
        public string? Role { get; set; } // e.g., "Patient", "Doctor", "Manager"
    }
}
