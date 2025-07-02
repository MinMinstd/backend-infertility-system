namespace infertility_system.Models
{
    public class TypeTest
    {
        public int TypeTestId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        // TypeTest N-1 ConsulationResult
        public int? ConsulationResultId { get; set; }

        public ConsulationResult? ConsulationResult { get; set; }

        // TypeTest N-1 TreatmentResult
        public int? TreatmentResultId { get; set; }

        public TreatmentResult? TreatmentResult { get; set; }
    }
}
