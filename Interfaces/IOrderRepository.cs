using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(int bookingId, int customerId, string wife, string husband);
        Task CreateOrderDetail(int orderId, int doctorId, int serviceId);
        Task<int> CountOrdersByCustomerId(int customerId);
    }
}