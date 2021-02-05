using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetClock.Application.Common.Options;

namespace NetClock.WebApi.Extensions.ConfigureServices
{
    public static class ConfigureAuthenticationExtension
    {
        public static IServiceCollection AddConfigureAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("Jwt");
            var jwtConfig = appSettingsSection.Get<JwtOptions>();
            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidIssuer = jwtConfig.ValidIssuer,
                        ValidAudience = jwtConfig.ValidAudience
                    };
                });

            return services;
        }
    }
}
