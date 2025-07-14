namespace infertility_system.Interfaces
{
    using infertility_system.Dtos.Embryo;
    using infertility_system.Models;

    public interface IEmbryoRepository
    {
        Task<ICollection<Embryo>> GetListEmbryosAsync(int userId);

        Task<List<Embryo>> GetEmbryosInDoctorAsync(int bookingId, int customerId);

        Task<bool> CreateEmbryoAsync(CreateEmbryoDto dto, int bookingId);

        Task<bool> UpdateEmbryoAsync(UpdateEmbryoDto dto, int embryoId);
    }
}
