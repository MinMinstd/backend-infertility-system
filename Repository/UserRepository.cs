namespace infertility_system.Repository
{
    using infertility_system.Data;
    using infertility_system.Interfaces;
    using infertility_system.Models;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CountCustomerAccount()
        {
            return await this.context.Users
                .CountAsync(u => u.Role == "Customer" && u.IsActive == true);
        }

        public async Task<int> CountDoctorsAccount()
        {
            return await this.context.Users
                .CountAsync(u => u.Role == "Doctor" && u.IsActive == true);
        }

        public async Task<int> CountNewAccount()
        {
            var fromDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-30)); // Assuming "new" means created in the last 30 days
            return await this.context.Users
                .Where(u => u.CreatedAt >= fromDate && (u.Role == "Customer" || u.Role == "Doctor") && u.IsActive == true)
                .CountAsync();
        }

        public async Task<int> CountTotalAccounts()
        {
            return await this.context.Users.CountAsync(u => (u.Role == "Customer" || u.Role == "Doctor") && u.IsActive == true);
        }

        public async Task DeleteUser(int userId)
        {
            var user = await this.context.Users.FindAsync(userId);
            if (user != null)
            {
                user.IsActive = false;
                await this.context.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetAllUsersForManagement()
        {
            return await this.context.Users
                .Where(u => (u.Role == "Customer" || u.Role == "Doctor") && u.IsActive == true)
                .Include(u => u.Customer)
                .Include(u => u.Doctor)
                .ToListAsync();
        }

        public async Task<User> GetUserAfterLogin(int userId)
        {
            return await this.context.Users.FindAsync(userId);
        }

        public async Task<User> UpdateUser(int id, User user)
        {
            var existingUser = await this.context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (existingUser == null)
            {
                return await Task.FromResult<User>(null);
            }

            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;
            await this.context.SaveChangesAsync();
            return await Task.FromResult(existingUser);
        }
    }
}
