namespace infertility_system.Models
{
    public class ConsulationResult
    {
        public int ConsulationResultId { get; set; }
        public DateOnly Date { get; set; }
        public string? ResultValue { get; set; }
        public string? Note { get; set; }

        //// ConsulationResult N-1 ConsulationRegistration
        //public int? ConsulationRegistrationId { get; set; }
        //public ConsulationRegistration? ConsulationRegistration { get; set; }

        // ConsulationResult 1-N MedicalRecordDetails
        public List<MedicalRecordDetail>? MedicalRecordDetails { get; set; }

        // ConsulationResult 1-N TypeTests
        public List<TypeTest>? TypeTests { get; set; }

        // ConsulationResult N-1 Booking
        public int? BookingId { get; set; }
        public Booking? Booking { get; set; }
    }
}
