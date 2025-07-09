using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IEmbryosRepository
    {
        Task<bool> CreateEmbryoAsync();

        Task<bool> UpdateEmbryoAsync();

        //Task<List<Embryo>> 
    }
}
