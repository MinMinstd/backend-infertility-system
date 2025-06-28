namespace infertility_system.Dtos.Booking
{
    public class BookingForListDto
    {
        public string CustomerName { get; set; }

        public string DoctorName { get; set; }

        public string Note { get; set; }

        public DateOnly Date { get; set; }

        public string Time { get; set; }

        public string Status { get; set; }
    }
}
