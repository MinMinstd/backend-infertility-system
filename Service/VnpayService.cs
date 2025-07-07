using infertility_system.Dtos.Vnpay;
using infertility_system.Helpers;
using infertility_system.Interfaces;

namespace infertility_system.Service
{
    public class VnpayService : IVnpayService
    {
        private readonly IConfiguration _configuration;

        public VnpayService (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreatePaymentUrl(VnpayPaymentInformationDto model, HttpContext context)
        {
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnpayHelper();
            var urlCallBack = _configuration["VNPay:vnp_ReturnUrl"];

            pay.AddRequestData("vnp_Version", _configuration["VNPay:vnp_Vesion"]);
            pay.AddRequestData("vnp_Command", _configuration["VNPay:vnp_Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["VNPay:vnp_TmnCode"]);
            pay.AddRequestData("vnp_Amount", ((int)model.Amount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["VNPay:vnp_CurCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["VNPay:vnp_Locate"]);
            pay.AddRequestData("vnp_OrderInfo", $"{model.Name} {model.OrderDescription} {model.Amount}");
            pay.AddRequestData("vnp_OrderType", model.OrderType);
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["VNPay:vnp_Url"], _configuration["VNPay:vnp_HashSecret"]);

            return paymentUrl;
        }


        public VnpayPaymentResponseDto PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnpayHelper();
            var response = pay.GetFullResponseData(collections, _configuration["VNPay:vnp_HashSecret"]);

            return response;
        }
    }
}
