using AutoMapper;
using infertility_system.Dtos.Payment;
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
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;


        public TreatementRoadmapController(ITreatementRoadmapRepository treatmentRoadmapRepository, IPaymentRepository paymentRepository, IMapper mapper)

        {
            _treatmentRoadmapRepository = treatmentRoadmapRepository;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAllTreatmentRoadMap")]
        public async Task<IActionResult> GetAllTreatmentRoadMap()
        {
            var listTreatmentRoadMap = await _treatmentRoadmapRepository.GetAllTreatmentRoadmapAsync();
            return Ok(_mapper.Map<List<ListTreatmentRoadMapDto>>(listTreatmentRoadMap));
        }


        [HttpGet("GetTreatementRoadmapWithPayment")]
        public async Task<IActionResult> GetTreatementRoadmapWithPayment(int month, int year)
        {
            var TreatementRoadmaps = _mapper.Map<List<TreamentRoadmapWithPaymentDto>>(await _treatmentRoadmapRepository.GetAllTreatmentRoadmapAsync());
            foreach (var treatement in TreatementRoadmaps) 
            {
                var payments = await _paymentRepository.GetListPaymentByMonthYearandIdTreatement(month, year, treatement.TreatmentRoadmapId);
                treatement.ListPayment = _mapper.Map<List<HistoryPaymentDto>>(payments);
                foreach(var payment in payments)
                {
                    treatement.Total += payment.PriceByTreatement;
                    treatement.Count += 1;
                }
            }
            return Ok(TreatementRoadmaps);
        }

        [HttpPost("AddTreatmentRoadMap")]
        public async Task<IActionResult> AddTreatmentRoadMap([FromBody] CreateTreatmentRoadmapDto requestTreatmentRoadmapDto)
        {
            if (requestTreatmentRoadmapDto == null)
            {
                return BadRequest("Invalid treatment roadmap data.");
            }

            if (string.IsNullOrEmpty(requestTreatmentRoadmapDto.Stage) ||
                string.IsNullOrEmpty(requestTreatmentRoadmapDto.Description) ||
                requestTreatmentRoadmapDto.DurationDay <= 0 ||
                requestTreatmentRoadmapDto.Price <= 0 ||
                requestTreatmentRoadmapDto.ServiceId <= 0)
            {
                return BadRequest("All fields are required and must be valid.");
            }

            var treatmentRoadmap = this._mapper.Map<Models.TreatmentRoadmap>(requestTreatmentRoadmapDto);
            await this._treatmentRoadmapRepository.AddTreatmentRoadmapAsync(treatmentRoadmap);
            return this.Ok("Treatment roadmap added successfully.");
        }

        [HttpGet("GetTreatmentRoadMapById/{treatmentRoadmapId}")]
        public async Task<IActionResult> GetTreatmentRoadMapById(int treatmentRoadmapId)
        {
            var treatmentRoadmap = await _treatmentRoadmapRepository.GetTreatmentRoadmapByIdAsync(treatmentRoadmapId);
            if (treatmentRoadmap == null)
            {
                return NotFound("Treatment roadmap not found.");
            }

            return Ok(_mapper.Map<UpdateTreatmentRoadmapDto>(treatmentRoadmap));
        }

        [HttpPut("UpdateTreatmentRoadMap")]
        public async Task<IActionResult> UpdateTreatmentRoadMap(int treatmentRoadmapId, [FromBody] UpdateTreatmentRoadmapDto requestTreatmentRoadmapDto)
        {
            if (requestTreatmentRoadmapDto == null)
            {
                return BadRequest("Invalid treatment roadmap data.");
            }

            if (string.IsNullOrEmpty(requestTreatmentRoadmapDto.Stage) ||
                string.IsNullOrEmpty(requestTreatmentRoadmapDto.Description) ||
                requestTreatmentRoadmapDto.DurationDay <= 0 ||
                requestTreatmentRoadmapDto.Price <= 0)
            {
                return BadRequest("All fields are required and must be valid.");
            }

            var treatmentRoadmap = _mapper.Map<Models.TreatmentRoadmap>(requestTreatmentRoadmapDto);
            var result = await _treatmentRoadmapRepository.UpdateTreatmentRoadmapAsync(treatmentRoadmapId, treatmentRoadmap);
            if (!result)
            {
                return NotFound("Treatment roadmap not found.");
            }

            return Ok("Treatment roadmap updated successfully.");

        }
    }
}
