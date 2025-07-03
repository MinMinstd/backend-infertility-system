namespace infertility_system.Dtos.ConsulationResult
{
    public class CreateConsultatioResultAndTypeTestDto
    {
        public DateOnly Date { get; set; }

        public string? ResultValue { get; set; }

        public string? Note { get; set; }

        public string? Name { get; set; }

        public string? DescriptionTypeTest { get; set; }
    }
}
