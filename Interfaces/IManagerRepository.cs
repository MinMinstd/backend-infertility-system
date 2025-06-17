using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IManagerRepository
    {
        Task<Manager> GetManagerAsync();
    }
}
