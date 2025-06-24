namespace infertility_system.Dtos.Customer
{
    public class CustomerInDoctorDto
    {
        public string? FullName { get; set; }
        public DateOnly Birthday { get; set; }
        public int? Age { get; set; }
        public string? Status { get; set; }
        public DateOnly StartDate { get; set; }
        public string? ServiceName { get; set; }
    }
}
