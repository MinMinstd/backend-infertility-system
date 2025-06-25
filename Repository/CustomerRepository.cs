using infertility_system.Data;
using infertility_system.Dtos.User;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;
        public CustomerRepository(AppDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto)
        {
            return await _authService.ChangePasswordAsync(userId, dto);
        }

        public async Task<bool> CheckCustomerExistsAsync(int userId)
        {
            return await _context.Customers.AnyAsync(x => x.UserId == userId);
        }

        public Task<bool> CheckExistsByUserId(int id)
        {
            return _context.Customers.AnyAsync(x => x.UserId == id);
        }

        public async Task<Customer> GetCustomersAsync(int userId)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<Doctor> GetDoctorDetailAsync(int doctorId)
        {
            var doctor = await _context.Doctors.Include(d => d.DoctorDegrees).
                FirstOrDefaultAsync(d => d.DoctorId == doctorId);
            if (doctor == null) return null;
            return doctor;
        }

        public async Task<List<Doctor>> GetListDoctorsAsync()
        {
            return await _context.Doctors.Include(d => d.DoctorDegrees).ToListAsync();
        }

        public async Task<Customer> UpdateCutomerAsync(int userId, Customer customer)
        {
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(x => x.UserId == userId);
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

            await _context.SaveChangesAsync();
            return existingCustomer;
        }
    }
}
