namespace infertility_system.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }

        public DateOnly Date { get; set; }

        public string? Name { get; set; }

        public string? Note { get; set; }

        // Prescription N-1 TreatmentResult
        public int TreatmentResultId { get; set; }

        public TreatmentResult? TreatmentResult { get; set; }

        // Prescription 1-N PrescriptionDetail
        public List<PrescriptionDetail>? PrescriptionDetails { get; set; }
    }
}
