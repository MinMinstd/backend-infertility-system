namespace infertility_system.Dtos.Booking
{
    public class BookingInCustomerDto
    {
        public DateOnly Date { get; set; }

        public string? Time { get; set; }

        public string? Type { get; set; }

        public string? Status { get; set; }

        public string? Note { get; set; }

        public string? FullName { get; set; }

        public string? Name { get; set; }
    }
}
