namespace infertility_system.Dtos.User
{
    public class UserRespondDto
    {
        public int UserId { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Role { get; set; } // Assuming Role is a string, adjust as necessary
    }
}
