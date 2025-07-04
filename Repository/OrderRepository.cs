namespace infertility_system.Repository
{
    using infertility_system.Data;
    using infertility_system.Interfaces;
    using infertility_system.Models;
    using Microsoft.EntityFrameworkCore;

    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;
        private readonly IManagerRepository managerRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly IOrderDetailRepository orderDetailRepository;

        public OrderRepository(AppDbContext context, IManagerRepository managerRepository, IDoctorRepository doctorRepository, IOrderDetailRepository orderDetailRepository)
        {
            this.context = context;
            this.managerRepository = managerRepository;
            this.doctorRepository = doctorRepository;
            this.orderDetailRepository = orderDetailRepository;
        }

        public async Task<int> CountOrdersByCustomerId(int customerId)
        {
            return await this.context.Orders.CountAsync(o => o.CustomerId == customerId);
        }

        public async Task<Order> CreateOrder(int bookingId, int customerId, string wife, string husband)
        {
            var order = new Order
            {
                BookingId = bookingId,
                Status = "Pending",
                CustomerId = customerId,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Time = TimeOnly.FromDateTime(DateTime.Now),
                Wife = wife,
                Husband = husband,
            };

            var manager = await this.managerRepository.GetManagerAsync();
            if (manager != null)
            {
                order.ManagerId = manager.ManagerId;
            }

            this.context.Orders.Add(order);
            await this.context.SaveChangesAsync();
            return order;
        }

        public async Task CreateOrderDetail(int orderId, int doctorId, int serviceId)
        {
            // var doctorName = await _context.Doctors
            //    .Where(d => d.DoctorId == doctorId)
            //    .Select(d => d.FullName)
            //    .FirstOrDefaultAsync();
            var doctor = await this.doctorRepository.GetDoctorByIdAsync(doctorId);

            var orderDetail = new OrderDetail
            {
                OrderId = orderId,
                DoctorName = doctor.FullName,
                ServiceId = serviceId,
                ServiceName = this.context.Services
                    .Where(s => s.ServiceDBId == serviceId)
                    .Select(s => s.Name)
                    .FirstOrDefault(),
            };

            // _context.OrderDetails.Add(orderDetail);
            // await _context.SaveChangesAsync();
            var createdOrderDetail = await this.orderDetailRepository.CreateOrderDetail(orderDetail);
        }
    }
}
