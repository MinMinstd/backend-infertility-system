namespace infertility_system.Models
{
    public class MedicalRecordDetail
    {
        public int MedicalRecordDetailId { get; set; }

        public DateOnly Date { get; set; }

        public string? TestResult { get; set; }

        public string? Note { get; set; }

        public string? TypeName { get; set; }

        public string? Status { get; set; }

        // MedicalRecordDetail N-1 MedicalRecord
        public int MedicalRecordId { get; set; }

        public MedicalRecord? MedicalRecord { get; set; }

        // MedicalRecordDetail N-1 ConsulationResult
        public int? ConsulationResultId { get; set; }

        public ConsulationResult? ConsulationResult { get; set; }

        // MedicalRecordDetail N-1 TreatmentResult
        public int? TreatmentResultId { get; set; }

        public TreatmentResult? TreatmentResult { get; set; }

        // MedicalRecordDetail N-1 TreatmentRoadmap
        public int? TreatmentRoadmapId { get; set; }

        public TreatmentRoadmap? TreatmentRoadmap { get; set; }
    }
}
