namespace infertility_system.Models
{
    public class Embryo
    {
        public int EmbryoId { get; set; }

        public DateOnly CreateAt { get; set; }

        public string? Quality { get; set; }

        public string? Type { get; set; }

        public int Amount { get; set; }

        // Embryo N-1 Customer
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }
    }
}
