namespace infertility_system.Interfaces
{
    using infertility_system.Models;

    public interface IOrderDetailRepository
    {
        Task<OrderDetail> CreateOrderDetail(OrderDetail orderDetail);
    }
}
