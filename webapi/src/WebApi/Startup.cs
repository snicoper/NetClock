using System.Globalization;
using System.Linq;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetClock.Application;
using NetClock.Application.Interfaces.Database;
using NetClock.Application.Localizations;
using NetClock.Domain;
using NetClock.Infrastructure;
using NetClock.Infrastructure.Persistence;
using NetClock.WebApi.Middlewares;
using Newtonsoft.Json;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace NetClock.WebApi
{
    public class Startup
    {
        private const string DefaultCors = "DefaultCors";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddDomain();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            // Routing.
            services.AddRouting(options => { options.LowercaseUrls = true; });

            // Razor.
            services.Configure<RazorViewEngineOptions>(options =>
            {
                // Vistas para emails.
                options.ViewLocationFormats.Add("~/Views/Emails/{0}.cshtml");
            });

            // Api versioning.
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            // MVC.
            services
                .AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IApplicationDbContext>())
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Culture = CultureInfo.CurrentCulture;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd'T'HH:mm:ss.FFFFFF'Z'";
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory)
                        => factory.Create(typeof(SharedLocalizer));
                })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddNewtonsoftJson();

            // Customise default API behaviour.
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureServices(services);

            // Add cors policy.
            services.AddCors(options =>
            {
                options.AddPolicy(DefaultCors, builder =>
                {
                    builder
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        public void ConfigureStagingServices(IServiceCollection services)
        {
            ConfigureServices(services);
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            ConfigureDevelopmentServices(services);
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureServices(services);

            // Add cors policy.
            services.AddCors(options =>
            {
                options.AddPolicy(DefaultCors, builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // Register the Swagger services.
            services.AddOpenApiDocument(document =>
            {
                // Add an authenticate button to Swagger for JWT tokens.
                document.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the text box: Bearer {your JWT token}."
                });
                document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));
                document.PostProcess = settings =>
                {
                    settings.Info.Version = "v1";
                    settings.Info.Title = "NetClock Clock API";
                    settings.Info.Description = "Registro de horas empleados de NetClock";
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Localization.
            var supportedCultures = new[] { "es" };

            app.UseRequestLocalization(options =>
            {
                options
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures)
                    .SetDefaultCulture("es");
            });

            app.UseCors(DefaultCors);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(CustomExceptionHandlerMiddleware));
            app.UseStaticFiles();
            app.UseHealthChecks("/health");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseOpenApi();
            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/swagger";
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
