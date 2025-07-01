using infertility_system.Dtos.Typetests;
using infertility_system.Models;

namespace infertility_system.Dtos.ConsulationResult
{
    public class ConsultationResultDto
    {
        public int ConsulationResultId { get; set; }

        public DateOnly Date { get; set; }

        public string? ResultValue { get; set; }

        public string? Note { get; set; }

        public List<TypeTestDto>? TypeTests { get; set; }
    }
}
