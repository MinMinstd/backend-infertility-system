namespace infertility_system.Dtos.TreatmentRoadmap
{
    public class UpdateMedicalRecordDetailDto
    {
        public DateOnly Date { get; set; }

        public string? TestResult { get; set; }

        public string? Note { get; set; }

        public string? Status { get; set; }
    }
}
