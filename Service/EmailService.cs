namespace infertility_system.Service
{
    using infertility_system.Dtos.Email;
    using infertility_system.Interfaces;
    using MailKit.Net.Smtp;
    using MimeKit;

    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration emailConfiguration;

        public EmailService(EmailConfiguration emailConfiguration)
        {
            this.emailConfiguration = emailConfiguration ?? throw new ArgumentNullException(nameof(emailConfiguration));
        }

        public Task SendEmail(EmailMessage emailMessage)
        {
            var mimeMessage = this.CreateEmailMessage(emailMessage);
            return this.SendEmailAsync(mimeMessage);
        }

        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(this.emailConfiguration.FromName, this.emailConfiguration.FromEmail));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = message.Content,
            };
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private async Task SendEmailAsync(MimeMessage mimeMessage)
        {
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(this.emailConfiguration.SmtpServer, this.emailConfiguration.SmtpPort, this.emailConfiguration.UseSSL);
                client.AuthenticationMechanisms.Remove("XOAUTH2"); // Remove XOAUTH2 if not used
                await client.AuthenticateAsync(this.emailConfiguration.SmtpUsername, this.emailConfiguration.SmtpPassword);
                await client.SendAsync(mimeMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}
