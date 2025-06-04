using FluentValidation;

namespace infertility_system.Dtos.Admin
{
    public class RegisterAdminDtoValidator : AbstractValidator<RegisterRequestFromAdminDto>
    {
        public RegisterAdminDtoValidator()
        {
            RuleFor(u => u.FullName)
                .NotEmpty().WithMessage("Họ và tên không được để trống")
                .Length(3, 50).WithMessage("Họ và tên phải có 3 đến 50 ký tự");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email không được để trống")
                .EmailAddress().WithMessage("Email không hợp lệ");

            RuleFor(u => u.Phone)
                .NotEmpty().WithMessage("Phone không được để trống")
                .Matches(@"^0\d{9,10}$").WithMessage("Số điện thoại không hợp lệ");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống")
                .Length(12).WithMessage("Mật khẩu không hợp lệ");

            RuleFor(u => u.Role)
                .NotEmpty().WithMessage("Role không được để trống");

            RuleFor(u => u.Experience)
                .NotEmpty().WithMessage("Experience không được để trống")
                .GreaterThan(0).WithMessage("Experience phải lớn hơn 0")
                .When(u => u.Role == "Doctor");

            RuleFor(u => u.Experience)
                .Equal(0).WithMessage("Chỉ bác sĩ mới được có kinh nghiệm")
                .When(u => u.Role != "Doctor");

            RuleFor(u => u.Address)
                .NotEmpty().WithMessage("Địa chỉ không được để trống");
        }
    }
}
