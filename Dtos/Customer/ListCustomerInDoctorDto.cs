namespace infertility_system.Dtos.Customer
{
    public class ListCustomerInDoctorDto
    {
        public int CustomerId { get; set; }

        public string? FullName { get; set; }

        public DateOnly Birthday { get; set; }

        public int? Age { get; set; }

        public string? ServiceName { get; set; }

        public string? DescriptionService { get; set; }
    }
}
