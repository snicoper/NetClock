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
            RuleFor(v => v.UserName).NotEmpty().MaximumLength(20).UniqueUserName(userManager, localizer);
            RuleFor(v => v.Email).NotEmpty().EmailAddress().UniqueEmail(userManager, localizer);
            RuleFor(v => v.Password).MinimumLength(6);
            RuleFor(v => v.ConfirmPassword)
                .Equal(v => v.Password)
                .WithMessage(localizer["Las contraseñas deben ser iguales"]);
            RuleFor(v => v.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(v => v.LastName).NotEmpty().MaximumLength(50);
        }
    }
}
