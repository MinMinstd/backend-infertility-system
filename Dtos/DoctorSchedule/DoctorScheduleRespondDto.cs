﻿namespace infertility_system.Dtos.DoctorSchedule
{
    public class DoctorScheduleRespondDto
    {
        public int DoctorScheduleId { get; set; }

        public DateOnly WorkDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public string? Status { get; set; }
    }
}
