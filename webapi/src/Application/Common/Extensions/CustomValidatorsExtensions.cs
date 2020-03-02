using FluentValidation;
using Microsoft.AspNetCore.Identity;
using NetClock.Application.Common.Constants;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Common.Extensions
{
    public static class CustomValidatorsExtensions
    {
        public static IRuleBuilderOptions<T, string> UniqueUserName<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            UserManager<ApplicationUser> userManager)
        {
            return ruleBuilder.MustAsync(async (username, cancellation) =>
                {
                    var user = await userManager.FindByNameAsync(username);

                    return user is null;
                })
                .WithMessage("'{PropertyName}' ya esta en uso.");
        }

        public static IRuleBuilderOptions<T, string> UniqueEmail<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            UserManager<ApplicationUser> userManager)
        {
            return ruleBuilder.MustAsync(async (email, cancellation) =>
                {
                    var user = await userManager.FindByEmailAsync(email);

                    return user is null;
                })
                .WithMessage("'{PropertyName}' ya esta en uso.");
        }

        public static IRuleBuilderOptions<T, string> SupportedCulture<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(m => SupportedCultures.GetCulture(m) != null)
                .WithMessage("'{PropertyName}' no valida.");
        }
    }
}
