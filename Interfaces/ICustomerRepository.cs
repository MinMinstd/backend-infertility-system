using infertility_system.Dtos.User;
using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync(int userId);

        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto);
        Task<ICollection<MedicalRecordDetail>> GetMedicalRecords(int userId);
        Task<ICollection<Embryo>> GetEmbryos(int userId);
        Task<bool> CheckExists(int id);
    }
}
