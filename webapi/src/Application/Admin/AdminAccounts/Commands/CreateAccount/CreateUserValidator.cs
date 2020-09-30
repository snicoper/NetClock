using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using NetClock.Application.Common.Extensions;
using NetClock.Application.Common.Localizations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Admin.AdminAccounts.Commands.CreateAccount
{
    public class CreateUserValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateUserValidator(IStringLocalizer<IdentityLocalizer> localizer, UserManager<ApplicationUser> userManager)
        {
            RuleFor(v => v.UserName)
                .NotEmpty()
                .MaximumLength(20)
                .UniqueUserName(userManager, localizer)
                .WithName(localizer["Nombre de usuario"]);

            RuleFor(v => v.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .WithName("Nombre");

            RuleFor(v => v.LastName)
                .NotEmpty()
                .MaximumLength(50)
                .WithName("Apellidos");

            RuleFor(v => v.Email)
                .NotEmpty()
                .EmailAddress()
                .UniqueEmail(userManager, localizer)
                .WithName("Correo electrónico");

            RuleFor(v => v.Password)
                .MinimumLength(6)
                .WithName("Contraseña");

            RuleFor(v => v.ConfirmPassword)
                .Equal(v => v.Password)
                .WithMessage(localizer["Las contraseñas deben ser iguales"]);
        }
    }
}
