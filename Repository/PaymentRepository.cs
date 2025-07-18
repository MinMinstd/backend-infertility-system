using infertility_system.Data;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;
        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Payment>> GetAllPayment()
        {
            return await _context.Payments
                    .Include(x => x.Order)
                        .ThenInclude(o => o.Customer)
                    .Include(x => x.TreatmentRoadmap)
                        .ThenInclude(tr => tr.Service)
                    .ToListAsync();
        }

        public async Task<List<Payment>> GetListPaymentByUserId(int userId)
        {
            return await _context.Payments
                    .Include(x => x.Order)
                        .ThenInclude(o => o.Customer)
                    .Include(x => x.TreatmentRoadmap)
                        .ThenInclude(tr => tr.Service)
                    .Where(x => x.Order.Customer.UserId == userId)
                    .ToListAsync();
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            return await _context.Payments
                    .Include(x => x.Order)
                        .ThenInclude(o => o.Customer)
                    .Include(x => x.TreatmentRoadmap)
                        .ThenInclude(tr => tr.Service)
                    .FirstOrDefaultAsync(x => x.PaymentId == id);
        }

        public async Task<Payment> GetPaymentByOrderId(int orderId)
        {
            return await _context.Payments
                .Include(x => x.TreatmentRoadmap)
                .Where(x => x.OrderId == orderId && x.Status == "Pending")
                .FirstOrDefaultAsync();
        }

        public async Task UpdateStatusPayment(int paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment != null && payment.Status == "Pending")
            {
                payment.Status = "Đã Thanh Toán";
                _context.Payments.Update(payment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Payment>> GetListPaymentByMonthYearandIdTreatement(int month, int year, int id)
        {
            return await _context.Payments.Where(x => x.Date.Month == month && x.Date.Year == year && x.TreatmentRoadmapId == id)
                .Include(x => x.Order)
                        .ThenInclude(o => o.Customer)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalRevenue(int month, int year)
        {
            return await _context.Payments
                .Where(x => x.Date.Month == month && x.Date.Year == year && x.Status == "Đã Thanh Toán")
                .SumAsync(x => x.PriceByTreatement);
        }

        public async Task<int> GetTotalTransactions(int month, int year)
        {
            return await _context.Payments
                .Where(x => x.Date.Month == month && x.Date.Year == year && x.Status == "Đã Thanh Toán")
                .CountAsync();
        }

        public async Task<int> GetTotalCustomers(int month, int year)
        {
            return await _context.Payments
                .Where(x => x.Date.Month == month && x.Date.Year == year && x.Status == "Đã Thanh Toán")
                .Select(x => x.Order.CustomerId)
                .Distinct()
                .CountAsync();
        }
    }
}
