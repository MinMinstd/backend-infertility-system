namespace infertility_system.Dtos.Booking
{
    public class BookingServiceDto
    {
        public DateOnly? Date { get; set; }

        public string? Time { get; set; }

        public string? Wife { get; set; }

        public string? Husband { get; set; }

        public int DoctorId { get; set; }

        public int DoctorScheduleId { get; set; }

        public int ServiceId { get; set; }
    }
}
