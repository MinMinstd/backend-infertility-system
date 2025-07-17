using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface ITreatementRoadmapRepository
    {
        Task<List<TreatmentRoadmap>> GetAllTreatmentRoadmapAsync();

        Task<List<TreatmentRoadmap>> GetTreatmentRoadmapsByServiceIdAsync(int serviceDBId);

        Task<TreatmentRoadmap> GetTreatmentRoadmapByIdAsync(int treatmentRoadmapId);

        Task AddTreatmentRoadmapAsync(TreatmentRoadmap treatmentRoadmap);

        Task<bool> UpdateTreatmentRoadmapAsync(int treatmentRoadmapId, TreatmentRoadmap treatmentRoadmap);
    }
}
