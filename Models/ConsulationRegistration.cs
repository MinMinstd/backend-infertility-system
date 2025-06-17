namespace infertility_system.Models
{
    public class ConsulationRegistration
    {
        public int ConsulationRegistrationId { get; set; }
        public string? Type { get; set; }
        public string? Note { get; set; }

        // ConsulationRegistration 1-N ConsulationResult
        public List<ConsulationResult>? ConsulationResult { get; set; }
    }
}
