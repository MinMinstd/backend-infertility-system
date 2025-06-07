namespace infertility_system.Dtos.MedicalRecord
{
    public class EmbryoDto
    {
        public DateOnly CreateAt { get; set; }
        public string? Quality { get; set; }
        public string? Type { get; set; }
        public int Amount { get; set; }
    }
}
