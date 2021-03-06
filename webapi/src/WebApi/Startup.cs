using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using NetClock.Application;
using NetClock.Application.Common.Constants;
using NetClock.Domain;
using NetClock.Infrastructure;
using NetClock.Infrastructure.Persistence;
using NetClock.WebApi.Extensions.Configure;
using NetClock.WebApi.Extensions.ConfigureServices;

namespace NetClock.WebApi
{
    public class Startup
    {
        private const string DefaultCors = "DefaultCors";

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        private IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            // Dependency injection.
            services.AddApplication()
                .AddInfrastructure(Configuration)
                .AddDomain()
                .AddWebApi()
                .AddCustomOptions(Configuration)
                .AddConfigureIdentity()
                .AddConfigureAuthentication(Configuration)
                .AddConfigureApiControllers()
                .AddConfigureCors(Environment, DefaultCors);

            if (!Environment.IsProduction())
            {
                services.AddConfigureSwagger();
            }

            // Localization.
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Configure supported languages.
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SupportedCultures = Cultures.Supported;
                options.SupportedUICultures = Cultures.Supported;
            });

            // Routing.
            services.AddRouting(options => { options.LowercaseUrls = true; });

            // HealthChecks.
            services
                .AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddDbContextCheck<ApplicationDbContext>();

            // Razor.
            services.Configure<RazorViewEngineOptions>(options =>
            {
                // Vistas para emails.
                options.ViewLocationFormats.Add("~/Views/Emails/{0}.cshtml");
            });

            // Customise default API behaviour.
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Api versioning.
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            // Prevents redirection when not authenticated.
            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    return Task.CompletedTask;
                };
            });

            if (Environment.IsDevelopment())
            {
                services.AddDatabaseDeveloperPageExceptionFilter();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseConfigureCulture();
            app.UseConfigureByEnvironment(env);
            app.UseCors(DefaultCors);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseConfigureEndpoints();
        }
    }
}
