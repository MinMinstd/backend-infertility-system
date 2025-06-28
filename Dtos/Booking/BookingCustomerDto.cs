namespace infertility_system.Dtos.Booking
{
    public class BookingCustomerDto
    {
        public DateOnly Date { get; set; }
        public string? Time { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }
    }
}
