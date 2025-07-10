using AutoMapper;
using infertility_system.Dtos.TreatmentRoadmap;
using infertility_system.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace infertility_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatementRoadmapController : ControllerBase
    {
        private readonly ITreatementRoadmapRepository _treatmentRoadmapRepository;
        private readonly IMapper _mapper;
        public TreatementRoadmapController(ITreatementRoadmapRepository treatmentRoadmapRepository, IMapper mapper)
        {
            _treatmentRoadmapRepository = treatmentRoadmapRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAllTreatmentRoadMap")]
        public async Task<IActionResult> GetAllTreatmentRoadMap()
        {
            var listTreatmentRoadMap = await _treatmentRoadmapRepository.GetAllTreatmentRoadmapAsync();
            return Ok(_mapper.Map<List<ListTreatmentRoadMapDto>>(listTreatmentRoadMap));
        }
    }
}
