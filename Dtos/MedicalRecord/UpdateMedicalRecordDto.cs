namespace infertility_system.Dtos.MedicalRecord
{
    public class UpdateMedicalRecordDto
    {
        public string? Stage { get; set; }

        public string? Diagnosis { get; set; }

        public string? Status { get; set; }

        public int Attempt { get; set; }
    }
}
