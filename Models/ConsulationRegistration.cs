namespace infertility_system.Models
{
    public class ConsulationRegistration
    {
        public int ConsulationRegistrationId { get; set; }
        public DateOnly Date { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
        public string? Note { get; set; }

        // ConsulationRegistration 1-N Bookings
        public List<Booking>? Bookings { get; set; }

        // ConsulationRegistration 1-1 ConsulationResult
        public ConsulationResult? ConsulationResult { get; set; }

        // ConsulationRegistration 1-1 OrderDetail
        public OrderDetail? OrderDetail { get; set; }
    }
}
