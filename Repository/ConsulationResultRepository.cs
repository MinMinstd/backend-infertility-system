using infertility_system.Data;
using infertility_system.Interfaces;
using infertility_system.Models;

namespace infertility_system.Repository
{
    public class ConsulationResultRepository : IConsulationResultRepository
    {
        private readonly AppDbContext _context;
        public ConsulationResultRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateConsulationResultAsync(ConsulationResult consulationResult)
        {
            _context.ConsulationResults.Add(consulationResult);
            await _context.SaveChangesAsync();
        }
    }
}
