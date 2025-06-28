namespace infertility_system.Interfaces
{
    using infertility_system.Helpers;
    using infertility_system.Models;

    public interface IServiceRepository
    {
        Task<List<ServiceDB>> GetListServicesAsync(QueryService query);

        Task<List<ServiceDB>> GetAllServiceToBooking();

        Task<List<ServiceDB>> GetServicesForManagement();
    }
}
