namespace infertility_system.Service
{
    using infertility_system.Data;
    using infertility_system.Interfaces;
    using infertility_system.Models;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        private readonly AppDbContext context;

        public UserService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await this.context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.IsActive == true);
        }
    }
}