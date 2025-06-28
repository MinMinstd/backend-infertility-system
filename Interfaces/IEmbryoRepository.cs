namespace infertility_system.Interfaces
{
    using infertility_system.Models;

    public interface IEmbryoRepository
    {
        Task<ICollection<Embryo>> GetListEmbryosAsync(int userId);
    }
}
