using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllPayment();

        Task<Payment> GetPaymentById(int id);

        Task<List<Payment>> GetListPaymentByUserId(int userId);

        Task<Payment> GetPaymentByOrderId(int orderId);

        Task UpdateStatusPayment(int paymentId);

        Task<List<Payment>> GetListPaymentByMonthYearandIdTreatement(int month, int year, int id);
    }
}
