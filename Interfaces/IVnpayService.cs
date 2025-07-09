using infertility_system.Dtos.Vnpay;

namespace infertility_system.Interfaces
{
    public interface IVnpayService
    {
        string CreatePaymentUrl(VnpayPaymentInformationDto model, HttpContext context);

        VnpayPaymentResponseDto PaymentExecute(IQueryCollection collections);

    }
}
