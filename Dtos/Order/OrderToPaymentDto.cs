namespace infertility_system.Dtos.Order
{
    public class OrderToPaymentDto
    {
        public string? Wife { get; set; }

        public string? Husband { get; set; }

        public DateOnly Birthday { get; set; }

        public int Age { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? ServiceName { get; set; }

        public string? StageName { get; set; }

        public int OrderId { get; set; }
    }
}
