using FluentValidation;

namespace infertility_system.Dtos.User
{
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(u => u.CurrentPassword)
                .NotEmpty().WithMessage("Mật khẩu không được để trống");

            RuleFor(u => u.NewPassword)
                .NotEmpty().WithMessage("Mật khẩu mới không được để trống")
                .Length(3,50).WithMessage("Mật khẩu không hợp lệ");

            RuleFor(u => u.ConfirmPassword)
                .NotEmpty().WithMessage("ConfirmPassword không được để trống")
                .Equal(u => u.NewPassword).WithMessage("ConfirmPassword phải giống với NewPassword");
        }
    }
}
