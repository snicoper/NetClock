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
            RuleFor(v => v.Code).NotEmpty();
            RuleFor(v => v.Password).NotEmpty();
            RuleFor(v => v.ConfirmPassword)
                .NotEmpty()
                .Equal(v => v.Password)
                .WithMessage(localizer["Las contraseñas deben ser iguales"]);
        }
    }
}
