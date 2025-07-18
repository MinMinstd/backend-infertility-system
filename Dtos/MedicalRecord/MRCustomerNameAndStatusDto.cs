namespace infertility_system.Dtos.MedicalRecord
{
    public class MRCustomerNameAndStatusDto
    {
        public int MedicalRecordId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? Stage { get; set; }

        public string? Diagnosis { get; set; }

        public string? Status { get; set; }

        public int Attempt { get; set; }

        public string? FullName { get; set; }

        public string? ServiceName { get; set; }
    }
}
