using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IConsulationResultRepository
    {
        Task CreateConsulationResultAsync(ConsulationResult consulationResult);
    }
}
