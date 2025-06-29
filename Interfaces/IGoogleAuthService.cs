namespace infertility_system.Interfaces
{
    using infertility_system.Dtos.User;

    public interface IGoogleAuthService
    {
        Task<GoogleUserInfo?> VerifyGoogleTokenAsync(string credential);
    }
} 