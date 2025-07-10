namespace infertility_system.Dtos.Payment
{
    public class PaymentOnPendingDto
    {
        public string Status { get; set; }

        public int TreatmentRoadmapId { get; set; }

        public string Stage { get; set; }

        public decimal Price { get; set; }

        public int PaymentId { get; set; }
    }
}
