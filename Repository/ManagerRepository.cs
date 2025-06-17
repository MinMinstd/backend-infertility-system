using infertility_system.Data;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly AppDbContext _context;
        public ManagerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Manager> GetManagerAsync()
        {
            return await _context.Managers.FirstOrDefaultAsync();
        }
    }
}
