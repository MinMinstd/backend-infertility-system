namespace infertility_system.Dtos.TreatmentRoadmap
{
    public class TreatmentRoadmapDetailDto
    {
        public int TreatmentRoadmapId { get; set; }

        public DateOnly Date { get; set; }

        public string? Stage { get; set; }

        public string? Description { get; set; }

        public int DurationDay { get; set; }

        public string? Status { get; set; }
    }
}
