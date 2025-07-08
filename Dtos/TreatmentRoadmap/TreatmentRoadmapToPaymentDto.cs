namespace infertility_system.Dtos.TreatmentRoadmap
{
    public class TreatmentRoadmapToPaymentDto
    {
        public int TreatmentRoadmapId { get; set; }

        public string? Stage { get; set; }

        public decimal Price { get; set; }
    }
}
