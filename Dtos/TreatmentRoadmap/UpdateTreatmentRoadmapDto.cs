namespace infertility_system.Dtos.TreatmentRoadmap
{
    public class UpdateTreatmentRoadmapDto
    {
        public string? Stage { get; set; }

        public string? Description { get; set; }

        public int DurationDay { get; set; }

        public decimal Price { get; set; }
    }
}
