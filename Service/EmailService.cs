using infertility_system.Dtos.Email;
using infertility_system.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace infertility_system.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;
        public EmailService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration ?? throw new ArgumentNullException(nameof(emailConfiguration));
        }

        public Task SendEmail(EmailMessage emailMessage)
        {
            var mimeMessage = CreateEmailMessage(emailMessage);
            return SendEmailAsync(mimeMessage);
        }

        private MimeMessage CreateEmailMessage(EmailMessage message) {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfiguration.FromName, _emailConfiguration.FromEmail));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = message.Content
            };
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private async Task SendEmailAsync(MimeMessage mimeMessage)
        {
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, _emailConfiguration.UseSSL);
                client.AuthenticationMechanisms.Remove("XOAUTH2"); // Remove XOAUTH2 if not used
                await client.AuthenticateAsync(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
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
