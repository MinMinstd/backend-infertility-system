using infertility_system.Data;
using infertility_system.Helpers;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;
        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ServiceDB>> GetListServicesAsync(QueryService query)
        {
            var services = _context.Services.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                services = services.Where(x => x.Name.Contains(query.Name));
            }
            return await services.ToListAsync();
        }

        public async Task<List<ServiceDB>> GetAllServiceToBooking()
        {
            return await _context.Services.ToListAsync();
        }
    }
}
