using infertility_system.Dtos.TypeTests;

namespace infertility_system.Dtos.ConsulationResult
{
    public class UpdateConsultationResultAndTypetestDto
    {
        public DateOnly Date { get; set; }

        public string? ResultValue { get; set; }

        public string? Note { get; set; }

        public List<UpdateTypeTestDto> TypeTest { get; set; }
    }
}
