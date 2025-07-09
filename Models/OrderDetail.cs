namespace infertility_system.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }

        public string? DoctorName { get; set; }

        public string? ServiceName { get; set; }

        public string? StageName { get; set; }

        public DateOnly? DateTreatment { get; set; }

        public string? TimeTreatment { get; set; }

        // OrderDetail N-1 Order
        public int? OrderId { get; set; }

        public Order? Order { get; set; }

        // OrderDetail N-1 Service
        public int? ServiceId { get; set; }

        public ServiceDB? Service { get; set; }
    }
}
