namespace infertility_system.Repository
{
    using infertility_system.Data;
    using infertility_system.Interfaces;
    using infertility_system.Models;
    using Microsoft.EntityFrameworkCore;

    public class ManagerRepository : IManagerRepository
    {
        private readonly AppDbContext context;

        public ManagerRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Manager> GetManagerAsync()
        {
            return await this.context.Managers.FirstOrDefaultAsync();
        }
    }
}
