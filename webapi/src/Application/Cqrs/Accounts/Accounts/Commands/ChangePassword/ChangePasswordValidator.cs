using FluentValidation;

namespace NetClock.Application.Cqrs.Accounts.Accounts.Commands.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
            RuleFor(v => v.NewPassword)
                .NotEmpty()
                .Equal(v => v.ConfirmPassword)
                .WithMessage("Las nuevas contraseñas no coinciden");
            RuleFor(v => v.ConfirmPassword)
                .NotEmpty()
                .Equal(v => v.NewPassword)
                .WithMessage("Las nuevas contraseñas no coinciden");
        }
    }
}
