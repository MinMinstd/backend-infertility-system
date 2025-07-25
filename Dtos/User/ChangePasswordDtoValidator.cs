﻿namespace infertility_system.Dtos.User
{
    using FluentValidation;

    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            this.RuleFor(u => u.CurrentPassword)
                .NotEmpty().WithMessage("Mật khẩu không được để trống");

            this.RuleFor(u => u.NewPassword)
                .NotEmpty().WithMessage("Mật khẩu không được bỏ trống")
                .MinimumLength(12).WithMessage("Mật khẩu tối thiểu 12 ký tự")
                .Matches("[A-Z]").WithMessage("Mật khẩu phải có chữ hoa")
                .Matches("[a-z]").WithMessage("Mật khẩu phải có chữ thường")
                .Matches("[0-9]").WithMessage("Mật khẩu phải có số")
                .Matches("[^a-zA-Z0-9]").WithMessage("Mật khẩu phải có ký tự đặc biệt");

            this.RuleFor(u => u.ConfirmPassword)
                .NotEmpty().WithMessage("ConfirmPassword không được để trống")
                .Equal(u => u.NewPassword).WithMessage("ConfirmPassword phải giống với NewPassword");
        }
    }
}
