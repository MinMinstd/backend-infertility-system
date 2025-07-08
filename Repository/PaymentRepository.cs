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
    }
}
