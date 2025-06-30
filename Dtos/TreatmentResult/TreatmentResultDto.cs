namespace infertility_system.Dtos.TreatmentResult
{
    public class TreatmentResultDto
    {
        public int StepNumber { get; set; }

        public DateOnly Date { get; set; }

        public string? Description { get; set; }
    }
}
