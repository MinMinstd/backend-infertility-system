using infertility_system.Data;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly AppDbContext _context;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IManagerRepository _managerRepository;
        public FeedbackRepository(AppDbContext context, ICustomerRepository customerRepository, IOrderRepository orderRepository, IManagerRepository managerRepository)
        {
            _context = context;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _managerRepository = managerRepository;
        }

        public async Task<List<Feedback>> GetFeedbacksAsync()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        public async Task<bool> SubmitFeedbackAsync(int userId, Feedback feedback)
        {
            var manager = await _managerRepository.GetManagerAsync();

            var customer = await _customerRepository.GetCustomersAsync(userId);

            int count = await _orderRepository.CountOrdersByCustomerId(customer.CustomerId);

            if (count > 0)
            {
                feedback.CustomerId = customer.CustomerId;
                feedback.Date = DateOnly.FromDateTime(DateTime.Now);
                feedback.Status = "Pending";
                feedback.ManagerId = manager.ManagerId;

                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateFeedbackStatusAsync(int feedbackId, string status)
        {
            var feedback = await _context.Feedbacks.FirstOrDefaultAsync(f => f.FeedbackId == feedbackId);
            if (feedback == null) return false;

            feedback.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
