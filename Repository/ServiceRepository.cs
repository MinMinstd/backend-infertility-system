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

        public async Task AddServiceAsync(ServiceDB service)
        {
            await this.context.Services.AddAsync(service);
            await this.context.SaveChangesAsync();
        }

        public async Task<bool> UpdateServiceAsync(int serviceDBId, ServiceDB service)
        {
            var existingService = await this.context.Services.FindAsync(serviceDBId);
            if (existingService == null)
            {
                return false;
            }

            existingService.Name = service.Name;
            existingService.Description = service.Description;

            this.context.Services.Update(existingService);
            return await this.context.SaveChangesAsync() > 0; // Returns true if any changes were saved
        }

        public async Task<ServiceDB?> GetServiceByIdAsync(int serviceDBId)
        {
            return await this.context.Services
                .Include(s => s.Manager)
                .Include(s => s.Doctors)
                .FirstOrDefaultAsync(s => s.ServiceDBId == serviceDBId);
        }
    }
}
