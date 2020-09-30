using FluentValidation;
using Microsoft.Extensions.Localization;
using NetClock.Application.Common.Localizations;

namespace NetClock.Application.Admin.AdminAccounts.Commands.UpdateAccount
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator(IStringLocalizer<IdentityLocalizer> localizer)
        {
            RuleFor(v => v.Id).NotEmpty();

            RuleFor(v => v.UserName)
                .NotEmpty()
                .MaximumLength(20)
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
                .WithName(localizer["Correo electrónico"]);
        }
    }
}
