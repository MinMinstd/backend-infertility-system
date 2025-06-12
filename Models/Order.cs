namespace infertility_system.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string? Status { get; set; }
        public string? Wife { get; set; }
        public string? Husband { get; set; }

        // Order N-1 Customer
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        // Order 1-1 Booking
        public int? BookingId { get; set; }
        public Booking? Booking { get; set; }

        // Order N-1 Manager
        public int? ManagerId { get; set; }
        public Manager? Manager { get; set; }

        // Order 1-N OrderDetail
        public List<OrderDetail>? OrderDetails { get; set; }

        // Order 1-N Payment
        public List<Payment>? Payments { get; set; }
    }
}
