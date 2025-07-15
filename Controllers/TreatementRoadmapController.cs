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
    }
}
