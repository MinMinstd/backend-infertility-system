using FluentValidation;

namespace infertility_system.Dtos.User
{
    public class RegitsterDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegitsterDtoValidator()
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
                .Length(3,50).WithMessage("Mật khẩu không hợp lệ");

            RuleFor(u => u.Gender)
                .NotEmpty().WithMessage("Giới tính không được để trống")
                .Length(1).WithMessage("Giới tính không hợp lệ");

            RuleFor(u => u.Birthday)
                .NotEmpty().WithMessage("Ngày sinh không được để trống")
                .Must(date => date.Date <= DateTime.Today)
                .WithMessage("Ngày sinh không hợp lệ");

            RuleFor(u => u.Address)
                .NotEmpty().WithMessage("Địa chỉ không được bỏ trống");
        }

        
    }
}
