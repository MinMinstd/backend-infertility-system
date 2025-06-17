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

        public BookingRepository(
            AppDbContext context,
            IMapper mapper,
            IOrderRepository orderRepository,
            IDoctorScheduleRepository doctorScheduleRepository)
        {
            _context = context;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _doctorScheduleRepository = doctorScheduleRepository;
        }


        public async Task<bool> CheckCustomerInBookingAsync(int customerId)
        {
            return await _context.Bookings.AnyAsync(x => x.CustomerId == customerId);

            //return await _context.Doctors.ToListAsync();
        }

        public async Task<List<DoctorSchedule>> GetDoctorScheduleAsync(int doctorId, DateOnly date)
        {
            return await _doctorScheduleRepository.GetSchedulesByDoctorAndDate(doctorId, date);
        }

        public async Task<bool> BookingConsulantAsync(BookingConsulantDto dto, int userId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.UserId == userId);
            if (customer == null) return false;

            var booking = _mapper.Map<Booking>(dto);
            booking.Status = "Pending";
            booking.CustomerId = customer.CustomerId;
            booking.Type = "Consultant";

            await _doctorScheduleRepository.UpdateScheduleStatus(dto.DoctorScheduleId, "Unavailable");

            booking.Time = $"{dto.StartTime.Value.ToString("HH:mm")} - {dto.EndTime.Value.ToString("HH:mm")}";

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            var order = await _orderRepository.CreateOrder(booking.BookingId, customer.CustomerId);
            await _orderRepository.CreateOrderDetail(order.OrderId, dto.DoctorId);

            return true;
        }

        public async Task<bool> BookingServiceAsync(BookingServiceDto dto, int userId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.UserId == userId);
            if (customer == null) return false;

            var booking = _mapper.Map<Booking>(dto);
            booking.Status = "Pending";
            booking.CustomerId = customer.CustomerId;
            booking.Type = "Service";

            await _doctorScheduleRepository.UpdateScheduleStatus(dto.DoctorScheduleId, "Unavailable");

            booking.Time = $"{dto.StartTime.Value.ToString("HH:mm")} - {dto.EndTime.Value.ToString("HH:mm")}";

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
    }
}