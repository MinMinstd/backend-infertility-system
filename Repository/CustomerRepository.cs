namespace infertility_system.Repository
{
    using infertility_system.Data;
    using infertility_system.Dtos.User;
    using infertility_system.Interfaces;
    using infertility_system.Models;
    using Microsoft.EntityFrameworkCore;

    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext context;
        private readonly IAuthService authService;

        public CustomerRepository(AppDbContext context, IAuthService authService)
        {
            this.context = context;
            this.authService = authService;
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto)
        {
            return await this.authService.ChangePasswordAsync(userId, dto);
        }

        public async Task<bool> CheckCustomerExistsAsync(int userId)
        {
            return await this.context.Customers.AnyAsync(x => x.UserId == userId);
        }

        public Task<bool> CheckExistsByUserId(int id)
        {
            return this.context.Customers.AnyAsync(x => x.UserId == id);
        }

        public async Task<List<Booking>> GetBookingsAsync(int userId)
        {
            var customer = await this.GetCustomersAsync(userId);

            var bookings = await this.context.Bookings
                        .Where(b => b.CustomerId == customer.CustomerId)
                        .Include(b => b.DoctorSchedule)
                        .ThenInclude(ds => ds.Doctor)
                        .ThenInclude(d => d.ServiceDB)
                        .ToListAsync();
            return bookings;
        }

        public async Task<Customer> GetCustomersAsync(int userId)
        {
            return await this.context.Customers.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<Doctor> GetDoctorDetailAsync(int doctorId)
        {
            var doctor = await this.context.Doctors.Include(d => d.DoctorDegrees).
                FirstOrDefaultAsync(d => d.DoctorId == doctorId);
            if (doctor == null)
            {
                return null;
            }

            return doctor;
        }

        public async Task<List<MedicalRecord>> GetInformationServiceAsync(int userId)
        {
            var customer = await this.GetCustomersAsync(userId);

            var medicalRecords = await this.context.MedicalRecords
                        .Where(m => m.CustomerId == customer.CustomerId)
                        .Include(m => m.Doctor)
                        .ThenInclude(d => d.ServiceDB)
                        .ToListAsync();
            return medicalRecords;

        }

        public async Task<List<OrderDetail>> GetListAppointmentAsync(int bookingId)
        {
            var booking = await this.context.Bookings.FirstOrDefaultAsync(b => b.BookingId == bookingId);

            var order = await this.context.Orders.FirstOrDefaultAsync(o => o.BookingId == booking.BookingId);

            var orderDetail = await this.context.OrderDetails
                        .Where(od => od.OrderId == order.OrderId)
                        .ToListAsync();
            return orderDetail;
        }

        public async Task<List<Doctor>> GetListDoctorsAsync()
        {
            return await this.context.Doctors.Include(d => d.DoctorDegrees).ToListAsync();
        }

        public async Task<Customer> UpdateCutomerAsync(int userId, Customer customer)
        {
            var existingCustomer = await this.context.Customers.FirstOrDefaultAsync(x => x.UserId == userId);
            if (existingCustomer == null)
            {
                return null;
            }

            existingCustomer.FullName = customer.FullName;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Gender = customer.Gender;
            existingCustomer.Address = customer.Address;
            existingCustomer.Birthday = customer.Birthday;

            await this.context.SaveChangesAsync();
            return existingCustomer;
        }
    }
}
