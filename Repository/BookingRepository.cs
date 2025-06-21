using AutoMapper;
using infertility_system.Data;
using infertility_system.Dtos.Booking;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IDoctorScheduleRepository _doctorScheduleRepository;
        private readonly ICustomerRepository _customerRepository;

        public BookingRepository(
            AppDbContext context,
            IMapper mapper,
            IOrderRepository orderRepository,
            IDoctorScheduleRepository doctorScheduleRepository,
            ICustomerRepository customerRepository)
        {
            _context = context;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _doctorScheduleRepository = doctorScheduleRepository;
            _customerRepository = customerRepository;
        }

        public async Task<List<DoctorSchedule>> GetDoctorScheduleAsync(int doctorId, DateOnly date)
        {
            return await _doctorScheduleRepository.GetSchedulesByDoctorAndDate(doctorId, date);
        }

        public async Task<bool> BookingConsulantAsync(BookingConsulantDto dto, int userId)
        {
            var customer = await _customerRepository.GetCustomersAsync(userId);
            if (customer == null) return false;

            var booking = _mapper.Map<Booking>(dto);
            booking.Status = "Pending";
            booking.CustomerId = customer.CustomerId;
            booking.Type = "Consultant";

            await _doctorScheduleRepository.UpdateScheduleStatus(dto.DoctorScheduleId, "Unavailable");

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            var order = await _orderRepository.CreateOrder(booking.BookingId, customer.CustomerId, null, null);
            await _orderRepository.CreateOrderDetail(order.OrderId, dto.DoctorId, dto.ServiceId);

            return true;
        }

        public async Task<bool> BookingServiceAsync(BookingServiceDto dto, int userId)
        {
            var customer = await _customerRepository.GetCustomersAsync(userId);
            if (customer == null) return false;

            var booking = _mapper.Map<Booking>(dto);
            booking.Status = "Pending";
            booking.CustomerId = customer.CustomerId;
            booking.Type = "Service";

            await _doctorScheduleRepository.UpdateScheduleStatus(dto.DoctorScheduleId, "Unavailable");

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            var order = await _orderRepository.CreateOrder(booking.BookingId, customer.CustomerId, dto.Wife, dto.Husband);
            await _orderRepository.CreateOrderDetail(order.OrderId, dto.DoctorId, dto.ServiceId);

            return true;
        }

        public Task<List<Doctor>> GetAllDoctorAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateBookingStatusAsync(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            booking.Status = "Completed";

            await _context.SaveChangesAsync();
        }

        public async Task<List<Booking>> GetListBooking()
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.DoctorSchedule)
                .ThenInclude(ds => ds.Doctor)
                .ToListAsync();
        }
    }
}