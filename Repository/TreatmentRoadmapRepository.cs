using infertility_system.Data;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class TreatmentRoadmapRepository : ITreatementRoadmapRepository
    {
        private readonly AppDbContext _context;

        public TreatmentRoadmapRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TreatmentRoadmap>> GetAllTreatmentRoadmapAsync()
        {
            return await _context.TreatmentRoadmaps
                .Include(x => x.Service)
                .ToListAsync();
        }
    }
}
