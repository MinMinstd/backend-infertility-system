namespace infertility_system.Dtos.Doctor
{
    using infertility_system.Dtos.DoctorDegree;

    public class DoctorForListDto
    {
        public int DoctorId { get; set; }

        public string? FullName { get; set; }

        public int Experience { get; set; }
    }
}
