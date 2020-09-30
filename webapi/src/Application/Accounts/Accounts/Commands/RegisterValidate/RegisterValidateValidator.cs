using FluentValidation;
using Microsoft.Extensions.Localization;
using NetClock.Application.Common.Localizations;

namespace NetClock.Application.Accounts.Accounts.Commands.RegisterValidate
{
    public class RegisterValidateValidator : AbstractValidator<RegisterValidateCommand>
    {
        public RegisterValidateValidator(IStringLocalizer<IdentityLocalizer> localizer)
        {
            RuleFor(r => r.UserId).NotEmpty();

            RuleFor(r => r.Code)
                .NotEmpty()
                .MinimumLength(2)
                .WithName(localizer["Código"]);
        }
    }
}
