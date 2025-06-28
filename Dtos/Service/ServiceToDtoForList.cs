namespace infertility_system.Dtos.Service
{
    public class ServiceToDtoForList
    {
        public int ServiceDBId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }
    }
}
