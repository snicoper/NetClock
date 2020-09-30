using FluentValidation;
using Microsoft.Extensions.Localization;
using NetClock.Application.Common.Localizations;

namespace NetClock.Application.Accounts.Accounts.Commands.ChangeEmailValidate
{
    public class ChangeEmailValidateValidator : AbstractValidator<ChangeEmailValidateCommand>
    {
        public ChangeEmailValidateValidator(IStringLocalizer<IdentityLocalizer> localizer)
        {
            RuleFor(v => v.UserId).NotEmpty();

            RuleFor(v => v.NewEmail)
                .NotEmpty()
                .EmailAddress()
                .WithName(localizer["Nuevo Email"]);

            RuleFor(v => v.Code)
                .NotEmpty()
                .WithName(localizer["Código"]);
        }
    }
}
