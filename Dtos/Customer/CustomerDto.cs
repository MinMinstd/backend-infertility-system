namespace infertility_system.Dtos.Customer
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string? Address { get; set; }
    }
}
