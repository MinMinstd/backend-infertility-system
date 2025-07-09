namespace infertility_system.Dtos.Payment
{
    public class PaymentDetailDto
    {
        public int PaymentId { get; set; }
        public string CustomerName { get; set; }
        public string ServiceName { get; set; }
        public DateOnly Date { get; set; }
        public decimal PriceByTreatement { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }
        public int TreatmentRoadmapId { get; set; }
        public string Stage { get; set; }
        public decimal Price { get; set; }
    }
}
