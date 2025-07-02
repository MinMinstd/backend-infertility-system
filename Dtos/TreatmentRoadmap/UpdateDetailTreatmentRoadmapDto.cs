using infertility_system.Models;

namespace infertility_system.Dtos.TreatmentRoadmap
{
    public class UpdateDetailTreatmentRoadmapDto
    {
        public DateOnly Date { get; set; }

        public string? Description { get; set; }

        public int DurationDay { get; set; }

        public string? Status { get; set; }
    }
}
