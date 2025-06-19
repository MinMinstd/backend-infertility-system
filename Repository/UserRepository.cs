using infertility_system.Data;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User> UpdateUser(int id, User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (existingUser == null)
            {
                return await Task.FromResult<User>(null);
            }
            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;
            await _context.SaveChangesAsync();
            return await Task.FromResult(existingUser);
        }
    }
}
