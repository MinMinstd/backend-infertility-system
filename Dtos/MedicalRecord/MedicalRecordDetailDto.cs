namespace infertility_system.Dtos.MedicalRecord
{
    public class MedicalRecordDetailDto
    {
        public int StepNumber { get; set; }

        public DateOnly Date { get; set; }

        public string? Note { get; set; }

        public string? TestResult { get; set; }

        public string? TypeName { get; set; }

        public string? Status { get; set; }

        public string? Stage { get; set; }
    }
}
