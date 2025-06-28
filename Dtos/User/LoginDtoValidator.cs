namespace infertility_system.Dtos.User
{
    using FluentValidation;

    public class LoginDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginDtoValidator()
        {
            this.RuleFor(u => u.Username)
                .NotEmpty().WithMessage("Tài khoản không được để trống");

            this.RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống");
        }
    }
}
