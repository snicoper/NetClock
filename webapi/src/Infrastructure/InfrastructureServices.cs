using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using NetClock.Application.Common.Configurations;
using NetClock.Application.Common.Interfaces.Database;
using NetClock.Application.Common.Localizations.Identity;
using NetClock.Domain.Entities.Identity;
using NetClock.Infrastructure.Persistence;

namespace NetClock.Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var assembly = typeof(ApplicationDbContext).Assembly.FullName;

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(assembly)));

            // DI.
            services.Scan(scan =>
                scan.FromCallingAssembly()
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            // HealthChecks.
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddDbContextCheck<ApplicationDbContext>();

            var appSettingsSection = configuration.GetSection("Jwt");
            var jwtConfig = appSettingsSection.Get<JwtConfig>();
            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

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
                        ValidIssuer = jwtConfig.ValidIssuer,
                        ValidAudience = jwtConfig.ValidAudience
                    };
                });

            // TODO: Experimental...
            // services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();

            return services;
        }
    }
}
