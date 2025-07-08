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

        public async Task<TreatmentRoadmap> GetTreatmentRoadmapByIdAsync(int treatmentRoadmapId, int serviceId)
        {
            return await _context.TreatmentRoadmaps
                .Where(tr => tr.TreatmentRoadmapId == treatmentRoadmapId && tr.ServiceId == serviceId)
                .FirstOrDefaultAsync();
        }
    }
}
