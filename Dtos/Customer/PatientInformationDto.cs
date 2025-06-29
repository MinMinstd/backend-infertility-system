namespace infertility_system.Dtos.Customer
{
    public class PatientInformationDto
    {
        public string? Wife { get; set; }

        public string? Husband { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public DateOnly Birthday { get; set; }

        public int? Age { get; set; }

        public string? Address { get; set; }
    }
}
