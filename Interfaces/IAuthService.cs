using infertility_system.Dtos.Admin;
using infertility_system.Dtos.User;
using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IAuthService
    {
        Task<string?> AuthenticateUserAsync(LoginRequestDto loginRequest);
        Task<User?> RegisterUserAsync(RegisterRequestDto user);
        Task<String?> RegisterDoctorAndManagerAsync(RegisterRequestFromAdminDto user);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto);
    }
}
