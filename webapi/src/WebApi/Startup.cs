using System.Globalization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NetClock.Application;
using NetClock.Application.Common.Interfaces.Database;
using NetClock.Application.Common.Localizations;
using NetClock.Domain;
using NetClock.Infrastructure;
using NetClock.Infrastructure.Persistence;
using NetClock.WebApi.Middlewares;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerUI;

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
            services.AddApplication(Configuration);
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
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            // Localization.
            services.AddLocalization(options => options.ResourcesPath = "Resources");

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
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "NetClock API", Version = "v1" });
                options.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme."
                    });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[] { }
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Localization.
            // @see: https://docs.microsoft.com/es-es/openspecs/windows_protocols/ms-lcid/a9eac961-e77d-41a6-90a5-ce1a8b0cdb9c
            var supportedCultures = new[] { "es-ES", "ca-ES", "en-GB" };

            app.UseRequestLocalization(options =>
            {
                options
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures)
                    .SetDefaultCulture("es-ES");
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
            app.UseHealthChecks("/health");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetClock v1");
                c.DocExpansion(DocExpansion.None);
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
