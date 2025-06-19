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

        public async Task<Customer> GetCustomersAsync(int userId)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
