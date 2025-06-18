using infertility_system.Data;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly IManagerRepository _managerRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderRepository(AppDbContext context, IManagerRepository managerRepository, IDoctorRepository doctorRepository, IOrderDetailRepository orderDetailRepository)
        {
            _context = context;
            _managerRepository = managerRepository;
            _doctorRepository = doctorRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<int> CountOrdersByCustomerId(int customerId)
        {
            return await _context.Orders.CountAsync(o => o.CustomerId == customerId);
        }

        public async Task<Order> CreateOrder(int bookingId, int customerId, string wife = null, string husband = null)
        {
            var order = new Order
            {
                BookingId = bookingId,
                Status = "Pending",
                CustomerId = customerId,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Time = TimeOnly.FromDateTime(DateTime.Now),
                Wife = wife,
                Husband = husband
            };

            var manager = await _managerRepository.GetManagerAsync();
            if (manager != null)
            {
                order.ManagerId = manager.ManagerId;
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task CreateOrderDetail(int orderId, int doctorId, int? serviceId = null)
        {
            //var doctorName = await _context.Doctors
            //    .Where(d => d.DoctorId == doctorId)
            //    .Select(d => d.FullName)
            //    .FirstOrDefaultAsync();
            var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);

            var orderDetail = new OrderDetail
            {
                OrderId = orderId,
                DoctorName = doctor.FullName,
                ServiceId = serviceId
            };
            //_context.OrderDetails.Add(orderDetail);
            //await _context.SaveChangesAsync();
            var createdOrderDetail = await _orderDetailRepository.CreateOrderDetail(orderDetail);
        }
    }
}
