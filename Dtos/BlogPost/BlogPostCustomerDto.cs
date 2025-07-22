namespace infertility_system.Dtos.BlogPost
{
    public class BlogPostCustomerDto
    {
        public int BlogPostId { get; set; }

        public string? Title { get; set; }

        public string? Story { get; set; }

        public string? TreatmentType { get; set; }

        public DateTime Date { get; set; } = DateTime.Now; // Default to current date

        public string? Image { get; set; }

        public string Status { get; set; } = "Pending"; // Default status

        public string CustomerName { get; set; } = string.Empty; // Customer name
    }
}
