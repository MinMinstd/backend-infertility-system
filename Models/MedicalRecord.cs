namespace infertility_system.Models
{
    public class MedicalRecord
    {
        public int MedicalRecordId { get; set; }
        public DateOnly Date { get; set; }
        public string? Stage { get; set; }
        public string? Diagnosis { get; set; }
        public string? Status { get; set; }

        // MedicalRecord 1-1 Customer
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        // Medical N-1 Doctor
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        // MedicalRecord 1-N MedicalRecordDetail
        public List<MedicalRecordDetail>? MedicalRecordDetails { get; set; }

        // MedicalRecord 1-1 TreatmentRoadmap
        public int TreatmentRoadmapId { get; set; }
        public TreatmentRoadmap? TreatmentRoadmaps { get; set; }
    }
}
