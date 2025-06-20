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

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu không được bỏ trống")
                .MinimumLength(9).WithMessage("Mật khẩu tối thiểu 12 ký tự")
                .Matches("[A-Z]").WithMessage("Mật khẩu phải có chữ hoa")
                .Matches("[a-z]").WithMessage("Mật khẩu phải có chữ thường")
                .Matches("[0-9]").WithMessage("Mật khẩu phải có số")
                .Matches("[^a-zA-Z0-9]").WithMessage("Mật khẩu phải có ký tự đặc biệt");

            RuleFor(u => u.Gender)
                .NotEmpty().WithMessage("Giới tính không được để trống")
                .Matches("Nam|Nữ").WithMessage("Giới tính phải là Nam hoặc Nữ");

            RuleFor(u => u.Birthday)
                .NotEmpty().WithMessage("Ngày sinh không được để trống")
                .Must(date => date <= DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Ngày sinh không hợp lệ");

            RuleFor(u => u.Address)
                .NotEmpty().WithMessage("Địa chỉ không được bỏ trống");
        }

        
    }
}
