using AutoMapper;
using infertility_system.Dtos.Payment;
using infertility_system.Interfaces;
using infertility_system.Repository;
using Microsoft.AspNetCore.Mvc;

namespace infertility_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        public PaymentController(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        [HttpGet("GetHistoryPayment")]
        public async Task<IActionResult> GetHistoryPayment() 
        {
            var listHistory = await _paymentRepository.GetAllPayment();
            return this.Ok(this._mapper.Map<List<HistoryPaymentDto>>(listHistory));
        }

        [HttpGet("GetPaymentDetail/{paymentId}")]
        public async Task<IActionResult> GetPaymentDetailById(int paymentId)
        {
            return null;
        }
    }
}
