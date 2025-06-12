namespace infertility_system.Dtos.DoctorSchedule
{
    public class DoctorScheduleDto
    {
        public DateOnly WorkDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
