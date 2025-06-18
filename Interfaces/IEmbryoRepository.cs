using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IEmbryoRepository
    {
        Task<ICollection<Embryo>> GetListEmbryosAsync(int userId);
    }
}
