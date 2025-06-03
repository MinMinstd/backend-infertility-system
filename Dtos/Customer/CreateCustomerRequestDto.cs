using System.Reflection.Metadata.Ecma335;

namespace infertility_system.Dtos.Customer
{
    public class CreateCustomerRequestDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public char Gender { get; set; }
        public DateOnly Birthday { get; set; }
        public string? Address { get; set; }
    }
}
