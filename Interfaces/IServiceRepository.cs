using infertility_system.Helpers;
using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IServiceRepository
    {
        Task<List<ServiceDB>> GetListServicesAsync(QueryService query);
        Task<List<ServiceDB>> GetAllServiceToBooking();
    }
}
