using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using NetClock.Application.Common.Localizations;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Common.Extensions
{
    public static class CustomValidatorsExtensions
    {
        public static IRuleBuilderOptions<T, string> UniqueUserName<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<IdentityLocalizer> localizer)
        {
            return ruleBuilder.MustAsync(async (username, cancellation) =>
                {
                    if (string.IsNullOrEmpty(username))
                    {
                        return false;
                    }

                    var user = await userManager.FindByNameAsync(username);

                    return user is null;
                })
                .WithMessage(localizer["El nombre de usuario ya esta en uso."]);
        }

        public static IRuleBuilderOptions<T, string> UniqueEmail<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<IdentityLocalizer> localizer)
        {
            return ruleBuilder.MustAsync(async (email, cancellation) =>
                {
                    if (string.IsNullOrEmpty(email))
                    {
                        return false;
                    }

                    var user = await userManager.FindByEmailAsync(email);

                    return user is null;
                })
                .WithMessage(localizer["El email ya esta en uso."]);
        }
    }
}
