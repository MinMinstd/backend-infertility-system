namespace infertility_system.Dtos.MedicalRecord
{
    public class UseServiceByCustomerDto
    {
        public string? FullName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? Status { get; set; }
        public string? NameService { get; set; }
        public string? Description { get; set; }

    }
}
