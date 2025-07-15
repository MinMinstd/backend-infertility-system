namespace infertility_system.Dtos.TreatmentRoadmap
{
    public class CreateTreatmentRoadmapDto
    {
        public string? Stage { get; set; }

        public string? Description { get; set; }

        public int DurationDay { get; set; }

        public decimal Price { get; set; }

        public int ServiceId { get; set; }
    }
}
