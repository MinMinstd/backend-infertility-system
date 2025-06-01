namespace infertility_system.Models
{
    public class ConsulationResult
    {
        public int ConsulationResultId { get; set; }
        public DateOnly Date { get; set; }
        public string? ResultValue { get; set; }
        public string? Note { get; set; }
        public int ConsulationRegistrationId { get; set; }

        // ConsulationResult 1-1 ConsulationRegistration
        public ConsulationRegistration? ConsulationRegistration { get; set; }

        // ConsulationResult 1-N MedicalRecordDetails
        public List<MedicalRecordDetail>? MedicalRecordDetails { get; set; }

        // ConsulationResult 1-N TypeTests
        public List<TypeTest>? TypeTests { get; set; }
    }
}
