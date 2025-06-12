namespace infertility_system.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public string? DoctorName { get; set; }
        public decimal? Price { get; set; }

        // OrderDetail N-1 Order
        public int? OrderId { get; set; }
        public Order? Order { get; set; }

        // OrderDetail N-1 Service
        public int? ServiceId { get; set; }
        public ServiceDB? Service { get; set; }

        // OrderDetail 1-1 ConsulationRegistration
        public int? ConsulationRegistrationId { get; set; }
        public ConsulationRegistration? ConsulationRegistration { get; set; }
    }
}
