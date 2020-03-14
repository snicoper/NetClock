using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Common.Localizations.Identity;
using NetClock.Domain.Entities.Identity;
using NetClock.Infrastructure.Persistence;

namespace NetClock.WebApi.Extensions.ConfigureServices
{
    public static class ConfigureIdentityExtension
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            // Identity.
            services
                .AddDefaultIdentity<ApplicationUser>(options =>
                {
                    // Configure identity options.
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<SpanishIdentityErrorDescriber>();

            return services;
        }
    }
}
