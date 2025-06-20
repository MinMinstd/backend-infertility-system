namespace infertility_system.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateOnly Date { get; set; }
        public string? Time { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }

        // Booking N-1 Customer
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }


        // Booking N-1 DoctorSchedule
        public int? DoctorScheduleId { get; set; }
        public DoctorSchedule? DoctorSchedule { get; set; }

        // Booking 1-1 Order
        public Order? Order { get; set; }

        // Booking 1-N ConsulationResult
        public List<ConsulationResult>? ConsulationResult { get; set; }
    }
}
