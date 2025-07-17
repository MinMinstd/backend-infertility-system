using infertility_system.Dtos.TypeTests;

namespace infertility_system.Dtos.TreatmentResult
{
    public class UpdateTreatmentResultAndTypetestDto
    {
        public DateOnly DateTreatmentResult { get; set; }

        public string? Description { get; set; }

        public string? Result { get; set; }

        public List<UpdateTypeTestDto>? TypeTest { get; set; }
    }
}
