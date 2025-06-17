using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IConsulationRegistrationRepository
    {
        Task<List<ConsulationRegistration>> GetAllRegistrationsAsync();
    }
}
