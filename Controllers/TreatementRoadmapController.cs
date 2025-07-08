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

        [HttpGet("GetTreatmentRoadmapById{treatmentRoadmapId}/{serviceId}")]
        public async Task<IActionResult> GetTreatmentRoadmap(int treatmentRoadmapId, int serviceId)
        {
            var treatmentRoadmap = await _treatmentRoadmapRepository.GetTreatmentRoadmapByIdAsync(treatmentRoadmapId, serviceId);
            if (treatmentRoadmap == null)
            {
                return NotFound();
            }
            var treatmentRoadmapToPaymentDto = _mapper.Map<TreatmentRoadmapToPaymentDto>(treatmentRoadmap);
            return Ok(treatmentRoadmapToPaymentDto);
        }
    }
}
