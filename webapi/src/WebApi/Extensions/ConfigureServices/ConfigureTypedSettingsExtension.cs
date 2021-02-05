using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Common.Options;

namespace NetClock.WebApi.Extensions.ConfigureServices
{
    public static class ConfigureTypedSettingsExtension
    {
        public static IServiceCollection AddStronglyTypeSettings(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure strongly typed settings objects.
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
            services.Configure<SmtpOptions>(configuration.GetSection("Smtp"));
            services.Configure<WebApiOptions>(configuration.GetSection("WebApi"));
            services.Configure<WebAppOptions>(configuration.GetSection("WebApp"));

            return services;
        }
    }
}
