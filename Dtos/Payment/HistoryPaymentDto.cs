namespace infertility_system.Dtos.Payment
{
    public class HistoryPaymentDto
    {
        public int PaymentId { get; set; }
        public string CustomerName { get; set; }
        public string ServiceName { get; set; }
        public DateOnly Date { get; set; }
        public decimal PriceByTreatement { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }
    }
}
