using infertility_system.Dtos.Typetests;
using infertility_system.Models;

namespace infertility_system.Dtos.MedicalRecord
{
    public class MedicalRecordDetailWithTypeTestDto
    {
        public DateOnly Date { get; set; }
        public string? TestResult { get; set; }
        public string? Note { get; set; }
        public string? TypeName { get; set; }
        public string? Status { get; set; }
        public int? TreatmentResultId { get; set; }
        public List<TypeTestDto>? TypeTest { get; set; }
    }
}
