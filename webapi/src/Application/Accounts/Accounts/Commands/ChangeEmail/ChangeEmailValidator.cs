using FluentValidation;
using Microsoft.Extensions.Localization;
using NetClock.Application.Common.Localizations;

namespace NetClock.Application.Accounts.Accounts.Commands.ChangeEmail
{
    public class ChangeEmailValidator : AbstractValidator<ChangeEmailCommand>
    {
        public ChangeEmailValidator(IStringLocalizer<IdentityLocalizer> localizer)
        {
            RuleFor(v => v.Id).NotEmpty();

            RuleFor(v => v.NewEmail)
                .NotEmpty()
                .EmailAddress()
                .WithName(localizer["Nuevo correo electrónico"]);
        }
    }
}
