using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using NetClock.Application.Common.Extensions;
using NetClock.Application.Common.Localizations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Accounts.Accounts.Commands.Register
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator(IStringLocalizer<IdentityLocalizer> localizer, UserManager<ApplicationUser> userManager)
        {
            RuleFor(v => v.UserName)
                .NotEmpty()
                .MaximumLength(20)
                .UniqueUserName(userManager, localizer)
                .WithName(localizer["Nombre de usuario"]);

            RuleFor(v => v.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .WithName(localizer["Nombre"]);

            RuleFor(v => v.LastName)
                .NotEmpty()
                .MaximumLength(50)
                .WithName(localizer["Apellidos"]);

            RuleFor(v => v.Email)
                .NotEmpty()
                .EmailAddress()
                .UniqueEmail(userManager, localizer)
                .WithName(localizer["Correo electrónico"]);

            RuleFor(v => v.Password)
                .MinimumLength(6)
                .WithName(localizer["Contraseña"]);

            RuleFor(v => v.ConfirmPassword)
                .Equal(v => v.Password)
                .WithName(localizer["Confirmar contraseña"])
                .WithMessage(localizer["Las contraseñas deben ser iguales"]);
        }
    }
}
