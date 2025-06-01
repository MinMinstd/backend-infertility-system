namespace infertility_system.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public char Gender { get; set; }
        public DateOnly Birthday { get; set; }
        public string? Address { get; set; }

        // Customer 1-N Embryo
        public List<Embryo>? Embryos { get; set; }

        // Customer 1-N Booking
        public List<Booking>? Bookings { get; set; }

        // Customer 1-N Feedback
        public List<Feedback>? Feedbacks { get; set; }

        // Customer 1-N BlogPost
        public List<BlogPost>? BlogPosts { get; set; }

        // Customer 1-1 MedicalRecord
        public MedicalRecord? MedicalRecord { get; set; }

        // Customer 1-N Order
        public List<Order>? Orders { get; set; }
    }
}
