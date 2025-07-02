namespace infertility_system.Dtos.TreatmentResult
{
    public class CreateTreatmentResultAndTypeTestDto
    {
        public DateOnly DateTreatmentResult { get; set; }

        public string? Stage { get; set; }

        public string? Description { get; set; }

        public int DurationDay { get; set; }

        public string? Result { get; set; }

        public int TreatmentRoadmapId { get; set; }

        public string? Name { get; set; }

        public string? DescriptionTypeTest { get; set; }
    }
}
