namespace infertility_system.Interfaces
{
    using infertility_system.Dtos.Email;

    public interface IEmailService
    {
        Task SendEmail(EmailMessage emailMessage);
    }
}
