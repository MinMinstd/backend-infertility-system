    using infertility_system.Dtos.Vnpay;
    using infertility_system.Helpers;
    using infertility_system.Interfaces;
    using infertility_system.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    namespace infertility_system.Controllers
    {
        [Route("api/vnpay")]
        [ApiController]
        public class VNpayPaymentController : ControllerBase
        {
            private readonly IVnpayService _vpnpayService;

            public VNpayPaymentController(IVnpayService vpnpayService)
            {
                _vpnpayService = vpnpayService;
            }

            [HttpPost("create")]
            public IActionResult CreatePaymentUrlVnpay(VnpayPaymentInformationDto paymentInfor)
            {
               var url = _vpnpayService.CreatePaymentUrl(paymentInfor, HttpContext);
                return Ok(url);
            }

            [HttpGet("return")]
            public IActionResult PaymentCallbackVnpay()
            {
                var response = _vpnpayService.PaymentExecute(Request.Query);

                var redirectUrl = $"http://localhost:5173/payment-result" +
                  $"?success={(response.Success ? "true" : "false")}" +
                  $"&orderId={response.OrderId}" +
                  $"&transactionId={response.TransactionId}" +
                  $"&message={Uri.EscapeDataString(response.OrderDescription ?? "Không có mô tả")}";

            return Redirect(redirectUrl);
            }
        }
    }
