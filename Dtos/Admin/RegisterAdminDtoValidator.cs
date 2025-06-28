namespace infertility_system.Dtos.Admin
{
    using FluentValidation;

    public class RegisterAdminDtoValidator : AbstractValidator<RegisterRequestFromAdminDto>
    {
        public RegisterAdminDtoValidator()
        {
            this.RuleFor(u => u.FullName)
                .NotEmpty().WithMessage("Họ và tên không được để trống")
                .Length(3, 50).WithMessage("Họ và tên phải có 3 đến 50 ký tự");

            this.RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email không được để trống")
                .EmailAddress().WithMessage("Email không hợp lệ");

            this.RuleFor(u => u.Phone)
                .NotEmpty().WithMessage("Phone không được để trống")
                .Matches(@"^0\d{9,10}$").WithMessage("Số điện thoại không hợp lệ");

            this.RuleFor(u => u.Password)
               .NotEmpty().WithMessage("Mật khẩu không được bỏ trống")
               .MinimumLength(12).WithMessage("Mật khẩu tối thiểu 12 ký tự")
               .Matches("[A-Z]").WithMessage("Mật khẩu phải có chữ hoa")
               .Matches("[a-z]").WithMessage("Mật khẩu phải có chữ thường")
               .Matches("[0-9]").WithMessage("Mật khẩu phải có số")
               .Matches("[^a-zA-Z0-9]").WithMessage("Mật khẩu phải có ký tự đặc biệt");

            this.RuleFor(u => u.Role)
                .NotEmpty().WithMessage("Role không được để trống");

            this.RuleFor(u => u.Experience)
                .NotEmpty().WithMessage("Experience không được để trống")
                .LessThanOrEqualTo(100).WithMessage("Experience phải lớn hơn 0 và nhỏ hơn bằng 100")
                .When(u => u.Role == "Doctor");

            this.RuleFor(u => u.Experience)
                .Equal(0).WithMessage("Chỉ bác sĩ mới được có kinh nghiệm")
                .When(u => u.Role != "Doctor");

            this.RuleFor(u => u.Address)
                .NotEmpty().WithMessage("Địa chỉ không được để trống");
        }
    }
}
