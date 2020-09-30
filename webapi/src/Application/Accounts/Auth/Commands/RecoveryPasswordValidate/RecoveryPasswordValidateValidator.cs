using FluentValidation;
using Microsoft.Extensions.Localization;
using NetClock.Application.Common.Localizations;

namespace NetClock.Application.Accounts.Auth.Commands.RecoveryPasswordValidate
{
    public class RecoveryPasswordValidateValidator : AbstractValidator<RecoveryPasswordValidateCommand>
    {
        public RecoveryPasswordValidateValidator(IStringLocalizer<IdentityLocalizer> localizer)
        {
            RuleFor(v => v.UserId).NotEmpty();

            RuleFor(v => v.Code)
                .NotEmpty()
                .WithName(localizer["Código"]);

            RuleFor(v => v.Password)
                .NotEmpty()
                .WithName(localizer["Contraseña"]);

            RuleFor(v => v.ConfirmPassword)
                .NotEmpty()
                .Equal(v => v.Password)
                .WithName(localizer["Confirmar contraseña"])
                .WithMessage(localizer["Las contraseñas deben ser iguales"]);
        }
    }
}
