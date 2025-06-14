using infertility_system.Data;
using infertility_system.Dtos.Feedback;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly AppDbContext _context;
        public FeedbackRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Feedback>> GetFeedbacksAsync()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        public async Task<bool> SubmitFeedbackAsync(int userId, FeedbackRequestDto dto)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.UserId == userId);
            if (customer == null) return false;

            int count = await _context.Orders.CountAsync(o => o.CustomerId == customer.CustomerId);

            if (count > 0)
            {
                var feedback = new Feedback
                {
                    CustomerId = customer.CustomerId,
                    Rating = dto.Rating,
                    Comments = dto.Comments,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Status = "Pending"
                };

                var manager = await _context.Managers.FirstOrDefaultAsync();
                if (manager != null)
                {
                    feedback.ManagerId = manager.ManagerId;
                }

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
