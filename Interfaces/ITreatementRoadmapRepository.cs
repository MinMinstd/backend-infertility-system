using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface ITreatementRoadmapRepository
    {
        Task<List<TreatmentRoadmap>> GetAllTreatmentRoadmapAsync();

        Task AddTreatmentRoadmapAsync(TreatmentRoadmap treatmentRoadmap);

        Task<TreatmentRoadmap> GetTreatmentRoadmapByIdAsync(int treatmentRoadmapId);

        Task<bool> UpdateTreatmentRoadmapAsync(int treatmentRoadmapId, TreatmentRoadmap treatmentRoadmap);
    }
}
