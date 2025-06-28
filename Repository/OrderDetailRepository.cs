namespace infertility_system.Repository
{
    using infertility_system.Data;
    using infertility_system.Interfaces;
    using infertility_system.Models;

    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly AppDbContext context;

        public OrderDetailRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<OrderDetail> CreateOrderDetail(OrderDetail orderDetail)
        {
            this.context.OrderDetails.Add(orderDetail);
            await this.context.SaveChangesAsync();
            return orderDetail;
        }
    }
}
