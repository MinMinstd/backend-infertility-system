namespace infertility_system.Dtos.MedicalRecordDetail
{
    public class CreateMedicalRecordDetailDto
    {
        public int? TreatmentRoadmapId { get; set; }

        public DateOnly Date { get; set; }

        public string? TypeName { get; set; }

        public string? TestResult { get; set; }

        public string? Note { get; set; }

        public string? Status { get; set; }
    }
}
