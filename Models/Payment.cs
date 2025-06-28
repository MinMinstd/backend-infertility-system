namespace infertility_system.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public decimal PriceByTreatement { get; set; }

        public string? Method { get; set; }

        public DateOnly Date { get; set; }

        public string? Status { get; set; }

        // Payment 1-1 TreatmentRoadmap
        public int TreatmentRoadmapId { get; set; }

        public TreatmentRoadmap? TreatmentRoadmap { get; set; }

        // Payment N-1 Order
        public int OrderId { get; set; }

        public Order? Order { get; set; }
    }
}
