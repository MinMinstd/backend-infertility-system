using MimeKit;

namespace infertility_system.Dtos.Email
{
    public class EmailMessage
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public EmailMessage(IEnumerable<String> to, String subject, String content) {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(email => new MailboxAddress(email,email)));
            Subject = subject;
            Content = content;
        }
    }
}
