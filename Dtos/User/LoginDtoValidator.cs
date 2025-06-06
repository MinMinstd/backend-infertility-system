using FluentValidation;

namespace infertility_system.Dtos.User
{
    public class LoginDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(u => u.Username)
                .NotEmpty().WithMessage("Tài khoản không được để trống");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống");
        }
    }
}
