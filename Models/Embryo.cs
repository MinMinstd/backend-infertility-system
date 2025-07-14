namespace infertility_system.Models
{
    public class Embryo
    {
        public int EmbryoId { get; set; }

        public DateOnly CreateAt { get; set; }

        public DateOnly? TransferredAt { get; set; }

        public string? Quality { get; set; }

        public string? Type { get; set; }

        public string? Status { get; set; }

        public string? Note { get; set; }

        // Embryo N-1 Customer
        public int OrderId { get; set; }

        public Order? Order { get; set; }
    }
}
