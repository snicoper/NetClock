using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NetClock.Application.Common.Configurations;
using NetClock.Application.Common.Interfaces.Database;
using NetClock.Domain.Entities.Identity;
using NetClock.Infrastructure.Persistence;
using NetClock.Infrastructure.Services.Validations;
using NetCore.AutoRegisterDi;

namespace NetClock.Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var assembly = typeof(ApplicationDbContext).Assembly.FullName;

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(assembly)));

            services
                .RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(ValidationFailureService)))
                .Where(c => c.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();

            ConfigureIdentity(services, configuration, environment);

            var identity = services
                .AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(assembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(assembly));
                });

            if (environment.IsDevelopment())
            {
                identity.AddDeveloperSigningCredential();
            }

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddDbContextCheck<ApplicationDbContext>();

            return services;
        }

        /// <summary>
        /// Configura toda la parte de identificación y autorización con identity.
        /// </summary>
        private static IServiceCollection ConfigureIdentity(
            IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            var appSettingsSection = configuration.GetSection("Jwt");
            var jwtConfig = appSettingsSection.Get<JwtConfig>();

            // Identity.
            services
                .AddDefaultIdentity<ApplicationUser>(options =>
                {
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
                .AddDefaultTokenProviders();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = "Cookies";

                    options.Authority = jwtConfig.ValidIssuer;
                    options.RequireHttpsMetadata = !environment.IsDevelopment();

                    options.UsePkce = true;
                    options.ClientId = "clock_client";
                    options.ClientSecret = "acf2ec6fb01a4b698ba240c2b10a0243";
                    options.ResponseType = "code";
                    options.ResponseMode = "form_post";
                    options.CallbackPath = "/signin-oidc";
                    options.SaveTokens = true;
                });

            // TODO: Experimental...
            // services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();

            return services;
        }
    }
}
