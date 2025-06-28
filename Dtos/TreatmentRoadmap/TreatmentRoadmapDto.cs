namespace infertility_system.Dtos.TreatmentRoadmap
{
    public class TreatmentRoadmapDto
    {
        public DateOnly Date { get; set; }
        public string? Stage { get; set; }
        public string? Description { get; set; }
        public int DurationDay { get; set; }
    }
}
