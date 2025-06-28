namespace infertility_system.Dtos.ConsulationResult
{
    public class ConsulationResultRequest
    {
        public DateOnly Date { get; set; }

        public string? ResultValue { get; set; }

        public string? Note { get; set; }

        public int? ConsulationRegistrationId { get; set; }

        public int? BookingId { get; set; }
    }
}
