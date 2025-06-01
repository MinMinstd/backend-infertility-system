namespace infertility_system.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        // Service 1-N OrderDetail
        public List<OrderDetail>? OrderDetails { get; set; }

        // Service 1-N Feedback
        public List<Feedback>? Feedbacks { get; set; }

        // Service 1-N TreatementRoadmap
        public List<TreatmentRoadmap>? TreatmentRoadmaps { get; set; }

        // Service N-1 Manager
        public int ManagerId { get; set; }
        public Manager? Manager { get; set; }
    }
}
