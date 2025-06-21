namespace infertility_system.Dtos.Feedback
{
    public class FeedbackResponseDto
    {
        public int FeedbackId { get; set; }
        public DateOnly Date { get; set; }
        public string FullName { get; set; }
        public string Comments { get; set; }
        public int Rating { get; set; }
        public string Status { get; set; }
    }
}
