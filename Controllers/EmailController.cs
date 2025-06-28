namespace infertility_system.Controllers
{
    using infertility_system.Dtos.Email;
    using infertility_system.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Controller]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService emailService;

        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
        {
            if (emailRequest == null)
            {
                return this.BadRequest("Email request cannot be null.");
            }

            try
            {
                var emailMessage = new EmailMessage(
                    new List<string> { emailRequest.To },
                    emailRequest.Subject,
                    emailRequest.Content);

                await this.emailService.SendEmail(emailMessage);
                return this.Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
