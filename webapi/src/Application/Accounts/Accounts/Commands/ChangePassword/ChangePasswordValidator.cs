using FluentValidation;
using Microsoft.Extensions.Localization;
using NetClock.Application.Common.Localizations;

namespace NetClock.Application.Accounts.Accounts.Commands.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator(IStringLocalizer<IdentityLocalizer> localizer)
        {
            RuleFor(v => v.Id).NotEmpty();

            RuleFor(v => v.OldPassword)
                .NotEmpty()
                .WithName(localizer["Contraseña actual"]);

            RuleFor(v => v.NewPassword)
                .NotEmpty()
                .Equal(v => v.ConfirmPassword)
                .WithName(localizer["Nueva contraseña"])
                .WithMessage("Las nuevas contraseñas no coinciden");

            RuleFor(v => v.ConfirmPassword)
                .NotEmpty()
                .Equal(v => v.NewPassword)
                .WithName(localizer["Repetir contraseña"])
                .WithMessage("Las nuevas contraseñas no coinciden");
        }
    }
}
