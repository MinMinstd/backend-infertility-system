namespace infertility_system.Dtos.Embryo
{
    public class CreateEmbryoDto
    {
        public DateOnly CreateAt { get; set; }

        public string? Quality { get; set; }

        public string? Status { get; set; }

        public string? Note { get; set; }
    }
}
