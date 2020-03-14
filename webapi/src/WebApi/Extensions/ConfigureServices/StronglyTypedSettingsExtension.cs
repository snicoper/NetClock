using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Common.Configurations;

namespace NetClock.WebApi.Extensions.ConfigureServices
{
    public static class StronglyTypedSettingsExtension
    {
        public static IServiceCollection AddStronglyTypeSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Configure strongly typed settings objects.
            services.Configure<JwtConfig>(configuration.GetSection("Jwt"));
            services.Configure<SmtpConfig>(configuration.GetSection("Smtp"));
            services.Configure<WebApiConfig>(configuration.GetSection("WebApi"));
            services.Configure<WebAppConfig>(configuration.GetSection("WebApp"));

            return services;
        }
    }
}
