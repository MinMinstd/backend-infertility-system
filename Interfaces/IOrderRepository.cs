namespace infertility_system.Interfaces
{
    using infertility_system.Models;

    public interface IOrderRepository
    {
        Task<Order> CreateOrder(int bookingId, int customerId, string? wife, string? husband);

        Task CreateOrderDetail(int orderId, int doctorId, int serviceId);

        Task<int> CountOrdersByCustomerId(int customerId);

        Task<Order> GetOrderCurrent(int customerId);
        Task<List<Order>> GetAllOrders();
    }
}