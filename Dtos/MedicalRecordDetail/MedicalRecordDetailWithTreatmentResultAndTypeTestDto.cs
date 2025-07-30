namespace infertility_system.Dtos.MedicalRecordDetail
{
    using infertility_system.Dtos.Typetests;
    using infertility_system.Models;

    public class MedicalRecordDetailWithTreatmentResultAndTypeTestDto
    {
        public DateOnly Date { get; set; }

        public string? TestResult { get; set; }

        public string? Note { get; set; }

        public string? TypeName { get; set; }

        public string? Status { get; set; }

        public string? StageName { get; set; }

        public int? TreatmentResultId { get; set; }

        //public DateOnly DateTreatmentResult { get; set; }

        public string? Stage { get; set; }

        public string? Description { get; set; }

        public int DurationDay { get; set; }

        public List<TypeTestDto>? TypeTest { get; set; }
    }
}
