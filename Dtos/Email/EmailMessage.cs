namespace infertility_system.Dtos.Email
{
    using MimeKit;

    public class EmailMessage
    {
        public List<MailboxAddress> To { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public EmailMessage(IEnumerable<string> to, string subject, string content)
        {
            this.To = new List<MailboxAddress>();
            this.To.AddRange(to.Select(email => new MailboxAddress(email, email)));
            this.Subject = subject;
            this.Content = content;
        }
    }
}
