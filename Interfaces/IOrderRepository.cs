using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(int bookingId, int customerId, string wife = null, string husband = null);
        Task CreateOrderDetail(int orderId, int doctorId, int? serviceId = null);
        Task<int> CountOrdersByCustomerId(int customerId);
    }
}