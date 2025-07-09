using infertility_system.Dtos.OrderDetail;

namespace infertility_system.Dtos.Order
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public string? Status { get; set; }

        public string? Wife { get; set; }

        public string? Husband { get; set; }

        public List<OrderDetailDto> orderDetailList { get; set; }
    }
}
