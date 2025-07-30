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

        public async Task AddTreatmentRoadmapAsync(TreatmentRoadmap treatmentRoadmap)
        {
            if (treatmentRoadmap == null)
            {
                throw new ArgumentNullException(nameof(treatmentRoadmap), "TreatmentRoadmap cannot be null");
            }

            treatmentRoadmap.Date = DateOnly.FromDateTime(DateTime.Now);
            _context.TreatmentRoadmaps.Add(treatmentRoadmap);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TreatmentRoadmap>> GetAllTreatmentRoadmapAsync()
        {
            return await _context.TreatmentRoadmaps
                .Include(x => x.Service)
                .ToListAsync();
        }

        public async Task<string> GetStageNameTreatmentRoadmapById(int treatmentRoadmapId)
        {
            var treatmentRoadmap = await _context.TreatmentRoadmaps.FirstOrDefaultAsync(tr => tr.TreatmentRoadmapId == treatmentRoadmapId);
            return treatmentRoadmap.Stage;
        }

        public async Task<TreatmentRoadmap> GetTreatmentRoadmapByIdAsync(int treatmentRoadmapId)
        {
            return await _context.TreatmentRoadmaps
                .Include(x => x.Service)
                .FirstOrDefaultAsync(x => x.TreatmentRoadmapId == treatmentRoadmapId);
        }

        public async Task<List<TreatmentRoadmap>> GetTreatmentRoadmapsByServiceIdAsync(int serviceDBId)
        {
            return await _context.TreatmentRoadmaps
                .Where(x => x.ServiceId == serviceDBId)
                .Include(x => x.Service)
                .ToListAsync();
        }

        public async Task<bool> UpdateTreatmentRoadmapAsync(int treatmentRoadmapId, TreatmentRoadmap treatmentRoadmap)
        {
            if (treatmentRoadmap == null)
            {
                throw new ArgumentNullException(nameof(treatmentRoadmap), "TreatmentRoadmap cannot be null");
            }

            var existingRoadmap = await _context.TreatmentRoadmaps.FindAsync(treatmentRoadmapId);
            if (existingRoadmap == null)
            {
                return false; // Not found
            }

            // Update properties
            existingRoadmap.Stage = treatmentRoadmap.Stage;
            existingRoadmap.Description = treatmentRoadmap.Description;
            existingRoadmap.DurationDay = treatmentRoadmap.DurationDay;
            existingRoadmap.Price = treatmentRoadmap.Price;

            _context.TreatmentRoadmaps.Update(existingRoadmap);
            await _context.SaveChangesAsync();
            return true; // Update successful
        }
    }
}
