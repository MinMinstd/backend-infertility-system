using AutoMapper;
using infertility_system.Dtos.Feedback;
using infertility_system.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace infertility_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;
        public FeedbackController(IFeedbackRepository feedbackRepository, IMapper mapper)
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetFeedbacks()
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var feedbacks = await _feedbackRepository.GetFeedbacksAsync();
            var feedbacksDto = _mapper.Map<List<FeedbackResponseDto>>(feedbacks);
            return Ok(feedbacksDto);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> SubmitFeedback([FromBody] FeedbackRequestDto feedbackRequest)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var feedback = _mapper.Map<Models.Feedback>(feedbackRequest);
            var result = await _feedbackRepository.SubmitFeedbackAsync(userId, feedback);
            return result
                ? Ok(new { message = "Feedback submitted successfully." })
                : BadRequest(new { message = "Failed to submit feedback. Please ensure you have placed an order." });
        }

        [HttpPut("{feedbackId}/status")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateFeedbackStatus(int feedbackId, [FromQuery] string status)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (status != "Ok" && status != "No")
            {
                return BadRequest(new { message = "Status must be either 'Ok' or 'No'." });
            }

            var result = await _feedbackRepository.UpdateFeedbackStatusAsync(feedbackId, status);
            return result
                ? Ok(new { message = "Feedback status updated successfully." })
                : BadRequest(new { message = "Failed to update feedback status. Feedback not found." });
        }
    }
}
