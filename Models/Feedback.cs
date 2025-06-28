namespace infertility_system.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }

        public DateOnly Date { get; set; }

        public string? Comments { get; set; }

        public string? FullName { get; set; }

        public int Rating { get; set; }

        public string? Status { get; set; }

        // Feedback N-1 Customer
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        //// Feedback N-1 Service
        // public int ServiceDBId { get; set; }
        // public ServiceDB? Service { get; set; }

        // Feedback N-1 Manager
        public int ManagerId { get; set; }

        public Manager? Manager { get; set; }
    }
}
