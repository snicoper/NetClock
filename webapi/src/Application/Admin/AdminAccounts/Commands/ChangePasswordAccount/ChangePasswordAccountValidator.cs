using FluentValidation;
using Microsoft.Extensions.Localization;
using NetClock.Application.Common.Localizations;

namespace NetClock.Application.Admin.AdminAccounts.Commands.ChangePasswordAccount
{
    public class ChangePasswordAccountValidator : AbstractValidator<ChangePasswordAccountCommand>
    {
        public ChangePasswordAccountValidator(IStringLocalizer<IdentityLocalizer> localizer)
        {
            RuleFor(v => v.Id).NotEmpty();

            RuleFor(v => v.NewPassword)
                .NotEmpty()
                .Equal(v => v.ConfirmPassword)
                .MinimumLength(6)
                .WithName(localizer["Nueva contraseña"]);

            RuleFor(v => v.ConfirmPassword)
                .NotEmpty()
                .WithName(localizer["Confirmar contraseña"]);
        }
    }
}
