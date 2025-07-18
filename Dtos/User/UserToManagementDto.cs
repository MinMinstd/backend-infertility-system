namespace infertility_system.Dtos.User
{
    public class UserToManagementDto
    {
        public int UserId { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? Role { get; set; } // e.g., "Patient", "Doctor", "Manager"

        public DateOnly CreatedAt { get; set; }

        public DateOnly LastActiveAt { get; set; }

        public int TotalActiveDays { get; set; } // Optional, for tracking active days

        public bool IsActive { get; set; } // Indicates if the user is currently active
    }
}
