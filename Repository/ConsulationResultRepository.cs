namespace infertility_system.Repository
{
    using infertility_system.Data;
    using infertility_system.Interfaces;
    using infertility_system.Models;

    public class ConsulationResultRepository : IConsulationResultRepository
    {
        private readonly AppDbContext context;

        public ConsulationResultRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task CreateConsulationResultAsync(ConsulationResult consulationResult)
        {
            this.context.ConsulationResults.Add(consulationResult);
            await this.context.SaveChangesAsync();
        }
    }
}
