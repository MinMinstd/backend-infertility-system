namespace infertility_system.Repository
{
    using AutoMapper;
    using infertility_system.Data;
    using infertility_system.Dtos.Booking;
    using infertility_system.Interfaces;
    using infertility_system.Models;
    using Microsoft.EntityFrameworkCore;

    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly IOrderRepository orderRepository;
        private readonly IDoctorScheduleRepository doctorScheduleRepository;
        private readonly ICustomerRepository customerRepository;

        public BookingRepository(
            AppDbContext context,
            IMapper mapper,
            IOrderRepository orderRepository,
            IDoctorScheduleRepository doctorScheduleRepository,
            ICustomerRepository customerRepository)
        {
            this.context = context;
            this.mapper = mapper;
            this.orderRepository = orderRepository;
            this.doctorScheduleRepository = doctorScheduleRepository;
            this.customerRepository = customerRepository;
        }

        public async Task<List<DoctorSchedule>> GetDoctorScheduleAsync(int doctorId, DateOnly date)
        {
            return await this.doctorScheduleRepository.GetSchedulesByDoctorAndDate(doctorId, date);
        }

        public async Task<bool> BookingConsulantAsync(BookingConsulantDto dto, int userId)
        {
            var customer = await this.customerRepository.GetCustomersAsync(userId);
            if (customer == null)
            {
                return false;
            }

            var booking = this.mapper.Map<Booking>(dto);
            booking.Status = "Pending";
            booking.CustomerId = customer.CustomerId;
            booking.Type = "Consulation";

            await this.doctorScheduleRepository.UpdateScheduleStatus(dto.DoctorScheduleId, "Unavailable");

            this.context.Bookings.Add(booking);
            await this.context.SaveChangesAsync();

            var order = await this.orderRepository.CreateOrder(booking.BookingId, customer.CustomerId, null, null);
            await this.orderRepository.CreateOrderDetail(order.OrderId, dto.DoctorId, dto.ServiceId, dto.Date, dto.Time, booking.Type);

            return true;
        }

        public async Task<bool> BookingServiceAsync(BookingServiceDto dto, int userId)
        {
            var customer = await this.customerRepository.GetCustomersAsync(userId);
            if (customer == null)
            {
                return false;
            }

            var booking = this.mapper.Map<Booking>(dto);
            booking.Status = "Pending";
            booking.CustomerId = customer.CustomerId;
            booking.Type = "Service";

            await this.doctorScheduleRepository.UpdateScheduleStatus(dto.DoctorScheduleId, "Unavailable");

            this.context.Bookings.Add(booking);
            await this.context.SaveChangesAsync();

            var order = await this.orderRepository.CreateOrder(booking.BookingId, customer.CustomerId, dto.Wife, dto.Husband);
            await this.orderRepository.CreateOrderDetail(order.OrderId, dto.DoctorId, dto.ServiceId, dto.Date, dto.Time, booking.Type);

            return true;
        }

        public Task<List<Doctor>> GetAllDoctorAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateBookingStatusAsync(int bookingId)
        {
            var booking = await this.context.Bookings.FindAsync(bookingId);
            booking.Status = "Completed";

            await this.context.SaveChangesAsync();
        }

        public async Task<List<Booking>> GetListBooking()
        {
            return await this.context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.DoctorSchedule)
                .ThenInclude(ds => ds.Doctor)
                .ToListAsync();
        }

        
    }
}