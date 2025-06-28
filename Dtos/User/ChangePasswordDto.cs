namespace infertility_system.Dtos.User
{
    public class ChangePasswordDto
    {
        public string? CurrentPassword { get; set; }

        public string? NewPassword { get; set; }

        public string? ConfirmPassword { get; set; }
    }
}
