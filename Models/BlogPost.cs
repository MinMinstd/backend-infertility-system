namespace infertility_system.Models
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }

        public string? Title { get; set; }

        public string? Story { get; set; }

        public string? TreatmentType { get; set; }

        public DateTime Date { get; set; } = DateTime.Now; // Default to current date

        public string? Image { get; set; }

        public string Status { get; set; } = "Pending"; // Default status

        // BlogPost N-1 Customer
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        // BlogPost N-1 Manager
        public int ManagerId { get; set; }

        public Manager? Manager { get; set; }
    }
}
