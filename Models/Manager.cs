    namespace infertility_system.Models
{
    public class Manager
    {
        public int UserId { get; set; }

        public int ManagerId { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        // Manager 1-N BlogPost
        public List<BlogPost>? BlogPosts { get; set; }

        // Manager 1-N Feedback
        public List<Feedback>? Feedbacks { get; set; }

        // Manager 1-N Order
        public List<Order>? Orders { get; set; }

        // Manager 1-N Service
        public List<ServiceDB>? Services { get; set; }

        // Manager 1-N DoctorSchedule
        public List<DoctorSchedule>? DoctorSchedules { get; set; }
    }
}
