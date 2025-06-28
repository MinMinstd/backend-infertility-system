namespace infertility_system.Interfaces
{
    using infertility_system.Models;

    public interface IConsulationResultRepository
    {
        Task CreateConsulationResultAsync(ConsulationResult consulationResult);
    }
}
