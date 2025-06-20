namespace infertility_system.Dtos.DoctorSchedule
{
    public class DoctorScheduleToBookingDto
    {
        public int DoctorScheduleId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
