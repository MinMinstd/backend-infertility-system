namespace infertility_system.Models
{
    public class TreatmentResult
    {
        public int TreatmentResultId { get; set; }

        public DateOnly DateTreatmentResult { get; set; }

        public string? Stage { get; set; }

        public string? Description { get; set; }

        public int DurationDay { get; set; }

        public string? Result { get; set; }

        // TreatmentResult N-1 TreatementRoadmap
        public int TreatmentRoadmapId { get; set; }

        public TreatmentRoadmap? TreatmentRoadmap { get; set; }

        // TreatmentResult 1-N TypeTest
        public List<TypeTest>? TypeTest { get; set; }

        // TreatmentResult 1-N Prescription
        public List<Prescription>? Prescriptions { get; set; }

        // TreatmentResult 1-N MedicalRecordDetail
        public List<MedicalRecordDetail>? MedicalRecordDetails { get; set; }
    }
}
