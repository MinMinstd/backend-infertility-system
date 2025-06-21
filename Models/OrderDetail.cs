namespace infertility_system.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public string? DoctorName { get; set; }
        public string? ServiceName { get; set; }

        // OrderDetail N-1 Order    
        public int? OrderId { get; set; }
        public Order? Order { get; set; }

        // OrderDetail N-1 Service
        public int? ServiceId { get; set; }
        public ServiceDB? Service { get; set; }
    }
}
