namespace infertility_system.Interfaces
{
    using infertility_system.Dtos.Admin;
    using infertility_system.Dtos.User;
    using infertility_system.Models;

    public interface IAuthService
    {
        Task<string?> AuthenticateUserAsync(LoginRequestDto loginRequest);

        Task<User?> RegisterUserAsync(RegisterRequestDto user);

        Task<string?> RegisterDoctorAndManagerAsync(RegisterRequestFromAdminDto user);

        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto);

        Task<bool> ConfirmEmailAsync(string token);
    }
}
