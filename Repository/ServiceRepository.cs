namespace infertility_system.Repository
{
    using infertility_system.Data;
    using infertility_system.Helpers;
    using infertility_system.Interfaces;
    using infertility_system.Models;
    using Microsoft.EntityFrameworkCore;

    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext context;

        public ServiceRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<ServiceDB>> GetListServicesAsync(QueryService query)
        {
            var services = this.context.Services.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                services = services.Where(x => x.Name.Contains(query.Name));
            }

            return await services.ToListAsync();
        }

        public async Task<List<ServiceDB>> GetAllServiceToBooking()
        {
            return await this.context.Services.ToListAsync();
        }

        public async Task<List<ServiceDB>> GetServicesForManagement()
        {
            return await this.context.Services.ToListAsync();
        }
    }
}
