namespace infertility_system.Dtos.TreatmentRoadmap
{
    public class ListTreatmentRoadMapDto
    {
        public int TreatmentRoadmapId { get; set; }
        public string Stage { get; set; }
        public string Description { get; set; }
        public int DurationDay { get; set; }
        public decimal Price { get; set; }
        public string ServiceName { get; set; }
    }
}
