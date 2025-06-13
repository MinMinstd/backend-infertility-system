namespace infertility_system.Dtos.Booking
{
    public class BookingDto
    {
        public string? Wife { get; set; }
        public string? Husband { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public string? Note { get; set; }
        public string? ServiceName { get; set; }
        public int DoctorId { get; set; }
        public int DoctorScheduleId { get; set; }
    }
}
