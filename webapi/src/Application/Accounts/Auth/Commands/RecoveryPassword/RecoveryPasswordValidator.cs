using FluentValidation;
using Microsoft.Extensions.Localization;
using NetClock.Application.Common.Localizations;

namespace NetClock.Application.Accounts.Auth.Commands.RecoveryPassword
{
    public class RecoveryPasswordValidator : AbstractValidator<RecoveryPasswordCommand>
    {
        public RecoveryPasswordValidator(IStringLocalizer<IdentityLocalizer> localizer)
        {
            RuleFor(v => v.Email)
                .NotEmpty()
                .EmailAddress()
                .WithName(localizer["Correo electrónico"]);
        }
    }
}
