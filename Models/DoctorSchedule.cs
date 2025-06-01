namespace infertility_system.Models
{
    public class DoctorSchedule
    {
        public int DoctorScheduleId { get; set; }
        public DateOnly WorkDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string? Status { get; set; }

        // DoctorSchedule N-1 Doctor
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }


        // DoctorSchedule N-1 Manager
        public int ManagerId { get; set; }
        public Manager? Manager { get; set; }

        // DoctorSchedule 1-N Booking
        public List<Booking>? Bookings { get; set; }
    }
}
