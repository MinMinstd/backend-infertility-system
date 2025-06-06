using infertility_system.Dtos.Service;
using infertility_system.Models;

namespace infertility_system.Mappers
{
    public static class ServiceMapper
    {
        public static ServiceToDtoForList ToDtoForList(this ServiceDB service)
        {
            if (service == null)
            {
                return null;
            }
            return new ServiceToDtoForList
            {
                ServiceDBId = service.ServiceDBId,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                ManagerId = service.ManagerId
            };
        }
    }
}
