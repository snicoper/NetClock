using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetClock.Application.Common.Authorization;
using NetClock.Application.Common.Configurations;
using NetClock.Application.Common.Interfaces.Database;
using NetClock.Application.Common.Localizations.Identity;
using NetClock.Domain.Entities.Identity;
using NetClock.Infrastructure.Persistence;
using NetClock.Infrastructure.Services.Validations;
using NetCore.AutoRegisterDi;

namespace NetClock.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services
                .RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(ValidationFailureService)))
                .Where(c => c.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            ConfigureIdentity(services, configuration);

            // Prevents redirection when not authenticated.
            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    return Task.CompletedTask;
                };
            });

            return services;
        }

        /// <summary>
        /// Configura toda la parte de identificación y autorización con identity.
        /// </summary>
        private static IServiceCollection ConfigureIdentity(IServiceCollection services, IConfiguration configuration)
        {
            // Configure strongly typed settings objects.
            var appSettingsSection = configuration.GetSection("Jwt");
            services.Configure<JwtConfig>(appSettingsSection);
            var appSettings = appSettingsSection.Get<JwtConfig>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

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
                        ValidIssuer = appSettings.ValidIssuer,
                        ValidAudience = appSettings.ValidAudience
                    };
                });

            // TODO: Experimental...
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();

            return services;
        }
    }
}
