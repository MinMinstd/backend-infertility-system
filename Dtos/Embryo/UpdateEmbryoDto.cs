namespace infertility_system.Dtos.Embryo
{
    public class UpdateEmbryoDto
    {
        public DateOnly? TransferredAt { get; set; }

        public string? Type { get; set; }

        public string? Status { get; set; }

        public string? Note { get; set; }
    }
}
