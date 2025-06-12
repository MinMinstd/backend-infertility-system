namespace infertility_system.Dtos.MedicalRecord
{
    public class MedicalRecordDetailDto
    {
        public string Stage {  get; set; }
        public string Note { get; set; }
        public string TestResult { get; set; }
        public string Type { get; set; }
        public int MedicalRecordId { get; set; }
        public int? ConsulationResultId { get; set; }
        public int? TreatmentResultId { get; set; }
        public int TreatmentRoadmapId { get; set; }
    }
}
