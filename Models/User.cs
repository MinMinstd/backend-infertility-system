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

        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        public DateOnly? LastActiveAt { get; set; }

        public int? TotalActiveDays { get; set; } // Optional, for tracking active days

        public virtual Customer? Customer { get; set; }

        public virtual Doctor? Doctor { get; set; }
    }
}
