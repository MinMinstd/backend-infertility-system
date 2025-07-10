using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface ITreatementRoadmapRepository
    {
        Task<List<TreatmentRoadmap>> GetAllTreatmentRoadmapAsync();
    }
}
