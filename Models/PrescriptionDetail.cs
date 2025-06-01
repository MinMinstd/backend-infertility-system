namespace infertility_system.Models
{
    public class PrescriptionDetail
    {
        public int PrescriptionDetailId { get; set; }
        public string? MedicineName { get; set; }
        public string? Dosage { get; set; }
        public string? DurationDay { get; set; }
        public string? Instruction { get; set; }

        // PrescriptionDetail N-1 Prescription
        public int PrescriptionId { get; set; }
        public Prescription? Prescription { get; set; }
    }
}
