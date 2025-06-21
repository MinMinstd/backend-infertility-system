using infertility_system.Dtos.Email;

namespace infertility_system.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(EmailMessage emailMessage);
    }
}
