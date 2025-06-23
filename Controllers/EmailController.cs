using infertility_system.Dtos.Email;
using infertility_system.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace infertility_system.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
            {
                _emailService = emailService;
            }
    
            [HttpPost("SendEmail")]
            public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
            {
                if (emailRequest == null)
                {
                    return BadRequest("Email request cannot be null.");
                }
    
                try
                {
                    var emailMessage = new EmailMessage
                    (
                        new List<String>{emailRequest.To},
                        emailRequest.Subject,   
                        emailRequest.Content
                    );
    
                    await _emailService.SendEmail(emailMessage);
                    return Ok("Email sent successfully.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
        }
    }
}
