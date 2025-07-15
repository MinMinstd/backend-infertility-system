using infertility_system.Dtos.Payment;

namespace infertility_system.Dtos.Customer
{
    public class CustomerWithListPaymentDto
    {
        public int UserId { get; set; }

        public int CustomerId { get; set; }

        public string? FullName { get; set; }

        public decimal? TotalPayment { get; set; } = 0;

        public List<HistoryPaymentDto> ListPayment { get; set; }
    }
}
