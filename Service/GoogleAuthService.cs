namespace infertility_system.Service
{
    using Google.Apis.Auth;
    using infertility_system.Dtos.User;
    using infertility_system.Interfaces;
    using Microsoft.Extensions.Configuration;

    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly IConfiguration configuration;

        public GoogleAuthService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<GoogleUserInfo?> VerifyGoogleTokenAsync(string credential)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { this.configuration["Google:ClientId"] },
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(credential, settings);

                return new GoogleUserInfo
                {
                    Email = payload.Email,
                    Name = payload.Name,
                    Picture = payload.Picture,
                    Sub = payload.Subject,
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}