namespace infertility_system.Interfaces
{
    using infertility_system.Models;

    public interface IFeedbackRepository
    {
        Task<List<Feedback>> GetFeedbacksAsync();

        Task<bool> SubmitFeedbackAsync(int userId, Feedback request);

        Task<bool> UpdateFeedbackStatusAsync(int feedbackId, string status);
    }
}
