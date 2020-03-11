using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetClock.Application;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Interfaces.Database;
using NetClock.Application.Common.Localizations;
using NetClock.Domain;
using NetClock.Infrastructure;
using NetClock.WebApi.Middlewares;
using Newtonsoft.Json;
using NSwag;
using NSwag.Generation.Processors.Security;

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

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication(Configuration);
            services.AddInfrastructure(Configuration);
            services.AddDomain();

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
                .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.Culture = CultureInfo.CurrentCulture;
                        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                        options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                        options.SerializerSettings.DateFormatString = "yyyy-MM-dd'T'HH:mm:ss.FFFFFF'Z'";
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IApplicationDbContext>())
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory)
                        => factory.Create(typeof(SharedLocalizer));
                })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);


            // Prevents redirection when not authenticated.
            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    return Task.CompletedTask;
                };
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

            // Add cors policy.
            services.AddCors(options =>
            {
                options.AddPolicy(DefaultCors, builder =>
                {
                    builder
                        .WithOrigins("http://localhost:4210")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
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
            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "NetClock API";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the text box: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Localization.
            // @see: https://docs.microsoft.com/es-es/openspecs/windows_protocols/ms-lcid/a9eac961-e77d-41a6-90a5-ce1a8b0cdb9c
            var supportedCultures = new[]
            {
                SupportedCultures.EsEs,
                SupportedCultures.EsCa,
                SupportedCultures.EnGb
            };

            app.UseRequestLocalization(options =>
            {
                options
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures)
                    .SetDefaultCulture(SupportedCultures.DefaultCulture);
            });

            app.UseCors(DefaultCors);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(CustomExceptionHandlerMiddleware));
            app.UseStaticFiles();

            app.UseOpenApi();
            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/swagger";
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
