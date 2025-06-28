namespace infertility_system.Repository
{
    using infertility_system.Data;
    using infertility_system.Interfaces;
    using infertility_system.Models;
    using Microsoft.EntityFrameworkCore;

    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly AppDbContext context;
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IManagerRepository managerRepository;

        public FeedbackRepository(AppDbContext context, ICustomerRepository customerRepository, IOrderRepository orderRepository, IManagerRepository managerRepository)
        {
            this.context = context;
            this.customerRepository = customerRepository;
            this.orderRepository = orderRepository;
            this.managerRepository = managerRepository;
        }

        public async Task<List<Feedback>> GetFeedbacksAsync()
        {
            return await this.context.Feedbacks.ToListAsync();
        }

        public async Task<bool> SubmitFeedbackAsync(int userId, Feedback feedback)
        {
            var manager = await this.managerRepository.GetManagerAsync();

            var customer = await this.customerRepository.GetCustomersAsync(userId);

            int count = await this.orderRepository.CountOrdersByCustomerId(customer.CustomerId);

            if (count > 0)
            {
                feedback.CustomerId = customer.CustomerId;
                feedback.Date = DateOnly.FromDateTime(DateTime.Now);
                feedback.Status = "Pending";
                feedback.ManagerId = manager.ManagerId;
                feedback.FullName = customer.FullName;

                this.context.Feedbacks.Add(feedback);
                await this.context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateFeedbackStatusAsync(int feedbackId, string status)
        {
            var feedback = await this.context.Feedbacks.FirstOrDefaultAsync(f => f.FeedbackId == feedbackId);
            if (feedback == null)
            {
                return false;
            }

            feedback.Status = status;
            await this.context.SaveChangesAsync();
            return true;
        }
    }
}
