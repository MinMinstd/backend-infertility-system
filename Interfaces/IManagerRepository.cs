namespace infertility_system.Interfaces
{
    using infertility_system.Models;

    public interface IManagerRepository
    {
        Task<Manager> GetManagerAsync();
    }
}
