using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllPayment();
    }
}
