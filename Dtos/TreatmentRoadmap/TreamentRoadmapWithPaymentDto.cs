using infertility_system.Dtos.Payment;

namespace infertility_system.Dtos.TreatmentRoadmap
{
    public class TreamentRoadmapWithPaymentDto
    {
        public int TreatmentRoadmapId { get; set; }

        public string? Stage { get; set; }

        public int Count { get; set; } = 0;

        public decimal Total { get; set; } = 0;

        public List<HistoryPaymentDto>? ListPayment { get; set; }
    }
}
