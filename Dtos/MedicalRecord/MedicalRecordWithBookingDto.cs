namespace infertility_system.Dtos.MedicalRecord
{
    public class MedicalRecordWithBookingDto
    {
        public int? BookingId { get; set; }

        public int MedicalRecordId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? Stage { get; set; }

        public string? Diagnosis { get; set; }

        public string? Status { get; set; }

        public int Attempt { get; set; } = 1;
    }
}
