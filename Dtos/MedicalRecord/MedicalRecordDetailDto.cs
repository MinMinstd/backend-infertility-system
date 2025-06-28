namespace infertility_system.Dtos.MedicalRecord
{
    public class MedicalRecordDetailDto
    {
        public DateOnly Date { get; set; }

        public string Note { get; set; }

        public string TestResult { get; set; }

        public string TypeName { get; set; }

        public int MedicalRecordId { get; set; }

        public int? ConsulationResultId { get; set; }

        public int? TreatmentResultId { get; set; }

        public int TreatmentRoadmapId { get; set; }
    }
}
