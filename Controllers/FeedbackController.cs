namespace infertility_system.Controllers
{
    using AutoMapper;
    using infertility_system.Dtos.Feedback;
    using infertility_system.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository feedbackRepository;
        private readonly IMapper mapper;

        public FeedbackController(IFeedbackRepository feedbackRepository, IMapper mapper)
        {
            this.feedbackRepository = feedbackRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedbacks()
        {
            var feedbacks = await this.feedbackRepository.GetFeedbacksAsync();
            var feedbacksDto = this.mapper.Map<List<FeedbackResponseDto>>(feedbacks);
            return this.Ok(feedbacksDto);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> SubmitFeedback([FromBody] FeedbackRequestDto feedbackRequest)
        {
            int userId = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var feedback = this.mapper.Map<Models.Feedback>(feedbackRequest);
            var result = await this.feedbackRepository.SubmitFeedbackAsync(userId, feedback);
            return result
                ? this.Ok(new { message = "Feedback submitted successfully." })
                : this.BadRequest(new { message = "Failed to submit feedback. Please ensure you have placed an order." });
        }

        [HttpPut("{feedbackId}/status")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateFeedbackStatus(int feedbackId, [FromQuery] string status)
        {
            var userRole = this.User.FindFirst(ClaimTypes.Role)?.Value;

            if (status != "Ok" && status != "No")
            {
                return this.BadRequest(new { message = "Status must be either 'Ok' or 'No'." });
            }

            var result = await this.feedbackRepository.UpdateFeedbackStatusAsync(feedbackId, status);
            return result
                ? this.Ok(new { message = "Feedback status updated successfully." })
                : this.BadRequest(new { message = "Failed to update feedback status. Feedback not found." });
        }
    }
}
