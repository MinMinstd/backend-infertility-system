using AutoMapper;
using infertility_system.Dtos.Payment;
using infertility_system.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet("GetAllHistoryPayment")]
        public async Task<IActionResult> GetHistoryPayment()
        {
            var listHistory = await _paymentRepository.GetAllPayment();
            return this.Ok(this._mapper.Map<List<HistoryPaymentDto>>(listHistory));
        }

        [HttpGet("GetHistoryPaymentByUserId")]
        [Authorize]
        public async Task<IActionResult> GetListPaymentById()
        {
            var userId = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var listPayment = await _paymentRepository.GetListPaymentByUserId(userId);
            return this.Ok(this._mapper.Map<List<HistoryPaymentDto>>(listPayment));
        }

        [HttpGet("GetPaymentDetail/{paymentId}")]
        public async Task<IActionResult> GetPaymentDetailById(int paymentId)
        {
            var payment = await _paymentRepository.GetPaymentById(paymentId);
            return this.Ok(this._mapper.Map<PaymentDetailDto>(payment));
        }

        [HttpGet("GetPaymentByOrderId/{orderId}")]
        public async Task<IActionResult> GetPaymentByOrderId(int orderId)
        {
            var payment = await _paymentRepository.GetPaymentByOrderId(orderId);
            if (payment == null)
            {
                return NotFound("Payment not found for the given order ID.");
            }
            return this.Ok(this._mapper.Map<PaymentOnPendingDto>(payment));
        }

        [HttpPut("UpdateStatusPayment/{paymentId}")]
        public async Task<IActionResult> UpdateStatusPayment(int paymentId)
        {
            await _paymentRepository.UpdateStatusPayment(paymentId);
            return this.Ok("Payment status updated successfully.");
        }

        [HttpGet("report/total-revenue/{month}/{year}")]
        public async Task<IActionResult> GetTotalRevenueByMonthAndYear(int month, int year)
        {
            var totalRevenue = await _paymentRepository.GetTotalRevenue(month, year);
            if (totalRevenue < 0)
            {
                return NotFound("No revenue found for the specified month and year.");
            }
            return this.Ok(new { TotalRevenue = totalRevenue });
        }

        [HttpGet("report/total-transaction/{month}/{year}")]
        public async Task<IActionResult> GetTotalTransactionByMonthAndYear(int month, int year)
        {
            var totalTransactions = await _paymentRepository.GetTotalTransactions(month, year);
            if (totalTransactions < 0)
            {
                return NotFound("No transactions found for the specified month and year.");
            }
            return this.Ok(new { TotalTransactions = totalTransactions });
        }

        [HttpGet("report/total-customer/{month}/{year}")]
        public async Task<IActionResult> GetTotalCustomerByMonthAndYear(int month, int year)
        {
            var totalCustomers = await _paymentRepository.GetTotalCustomers(month, year);
            if (totalCustomers < 0)
            {
                return NotFound("No customers found for the specified month and year.");
            }
            return this.Ok(new { TotalCustomers = totalCustomers });
        }
    }
}
