using infertility_system.Dtos.Typetests;

namespace infertility_system.Dtos.TreatmentResult
{
    public class TreatmentResultDto
    {
        public int TreatmentResultId { get; set; }

        public int StepNumber { get; set; }

        public DateOnly Date { get; set; }

        public string? Description { get; set; }

        public string? Result { get; set; }

        public List<TypeTestDto>? TypeTest { get; set; }
    }
}
