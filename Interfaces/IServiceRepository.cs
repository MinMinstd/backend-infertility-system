using infertility_system.Helpers;
using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IServiceRepository
    {
        public Task<List<ServiceDB>> GetAllServicesAsync(QueryService query);
    }
}
