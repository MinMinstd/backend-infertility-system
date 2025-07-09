namespace infertility_system.Dtos.OrderDetail
{
    public class ListAppointmentDto
    {
        public string? DoctorName { get; set; }

        public string? ServiceName { get; set; }

        public string? StageName { get; set; }

        public DateOnly? DateTreatment { get; set; }

        public string? TimeTreatment { get; set; }
    }
}
