using infertility_system.Helpers;
using infertility_system.Models;
using Microsoft.AspNetCore.Mvc;

namespace infertility_system.Controllers
{
    [Route("api/vnpay")]
    [ApiController]
    public class VNpayPaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public VNpayPaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("create")]
        public IActionResult CreatePaymentUrl(decimal payment)
        {
            var vnp_Url = _configuration["VNPay:vnp_Url"];
            var vnp_ReturnUrl = _configuration["VNPay:vnp_ReturnUrl"];
            var vnp_TmnCode = _configuration["VNPay:vnp_TmnCode"];
            var vnp_HashSecret = _configuration["VNPay:vnp_HashSecret"];

            var vnp_TxnRef = DateTime.Now.Ticks.ToString();
            var vnp_OrderInfo = "Thanh toán đơn hàng #" + vnp_TxnRef;
            var vnp_Amount = ((int)(payment * 100)).ToString(); // VNPay dùng đơn vị là VND * 100
            var vnp_CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var vnp_IpAddr = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";

            var vnp_Params = new SortedList<string, string>
            {
                { "vnp_Version", "2.1.0" },
                { "vnp_Command", "pay" },
                { "vnp_TmnCode", vnp_TmnCode },
                { "vnp_Amount", vnp_Amount },
                { "vnp_CurrCode", "VND" },
                { "vnp_TxnRef", vnp_TxnRef },
                { "vnp_OrderInfo", vnp_OrderInfo },
                { "vnp_OrderType", "other" },
                { "vnp_Locale", "vn" },
                { "vnp_ReturnUrl", vnp_ReturnUrl },
                { "vnp_IpAddr", vnp_IpAddr },
                { "vnp_CreateDate", vnp_CreateDate }
            };

            // 1. Tạo raw data để ký
            var signData = VnpayHelper.BuildQuery(vnp_Params);
            var vnp_SecureHash = VnpayHelper.HmacSHA512(vnp_HashSecret, signData);

            // 2. Thêm vào query string
            var queryString = $"{signData}&vnp_SecureHash={vnp_SecureHash}";
            var paymentUrl = $"{vnp_Url}?{queryString}";

            return Ok(new { paymentUrl });
        }

        [HttpGet("return")]
        public IActionResult VNPayReturn()
        {
            var vnp_HashSecret = _configuration["VNPay:vnp_HashSecret"];

            var query = HttpContext.Request.Query;
            var vnpData = new SortedList<string, string>();

            foreach (var key in query.Keys)
            {
                if (key.StartsWith("vnp_") && key != "vnp_SecureHash")
                {
                    vnpData.Add(key, query[key]);
                }
            }

            var rawData = VnpayHelper.BuildQuery(vnpData);
            var secureHash = VnpayHelper.HmacSHA512(vnp_HashSecret, rawData);
            var receivedHash = query["vnp_SecureHash"];

            if (secureHash != receivedHash) return BadRequest("Chữ ký không hợp lệ!");

            var responseCode = query["vnp_ResponseCode"];
            var transactionId = query["vnp_TxnRef"];

            if (responseCode == "00")
            {
                // ✅ Thanh toán thành công – cập nhật đơn hàng nếu cần
                return Ok($"Thanh toán thành công cho đơn hàng {transactionId}");
            }
            else
            {
                // ❌ Thất bại
                return BadRequest("Thanh toán thất bại!");
            }
        }
    }
}
