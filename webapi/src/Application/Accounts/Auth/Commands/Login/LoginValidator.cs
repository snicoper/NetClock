using FluentValidation;
using Microsoft.Extensions.Localization;
using NetClock.Application.Common.Localizations;

namespace NetClock.Application.Accounts.Auth.Commands.Login
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator(IStringLocalizer<IdentityLocalizer> localizer)
        {
            RuleFor(v => v.UserName)
                .NotEmpty()
                .WithName(localizer["Nombre de usuario"]);

            RuleFor(v => v.Password)
                .NotEmpty()
                .WithName(localizer["Contraseña"]);
        }
    }
}
