using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task<OrderDetail> CreateOrderDetail(OrderDetail orderDetail);
    }
}
