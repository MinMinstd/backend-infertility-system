namespace infertility_system.Dtos.Booking
{
    public class CreateBookingCustomerDto
    {
        public int TreatmentRoadmapId { get; set; }

        public DateOnly? DateTreatment { get; set; }

        public string? TimeTreatment { get; set; }
    }
}
