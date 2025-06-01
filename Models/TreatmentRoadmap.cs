namespace infertility_system.Models
{
    public class TreatmentRoadmap
    {
        public int TreatmentRoadmapId { get; set; }
        public DateOnly Date { get; set; }
        public string? Stage { get; set; }
        public string? Description { get; set; }
        public int DurationDay { get; set; }
        public decimal Price { get; set; }

        // TreatmentRoadmap N-1 Service
        public int ServiceId { get; set; }
        public ServiceDB? Service { get; set; }

        // TreatmentRoadmap 1-1 MedicalRecord
        public MedicalRecord? MedicalRecord { get; set; }

        // TreatmentRoadmap 1-1 Payment
        public Payment? Payment { get; set; }

        // TreatmentRoadmap 1-N TreatmentResult
        public List<TreatmentResult>? TreatmentResults { get; set; }
    }
}
