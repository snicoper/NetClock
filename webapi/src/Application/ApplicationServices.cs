using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Common.Behaviours;
using NetClock.Application.Common.Configurations;
using NetClock.Application.Common.Interfaces.Http;
using NetClock.Application.Common.Services.Http;

namespace NetClock.Application
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IResponseDataService<,>), typeof(ResponseDataService<,>));
            services.AddHttpContextAccessor();

            // Configure strongly typed settings objects.
            services.Configure<JwtConfig>(configuration.GetSection("Jwt"));
            services.Configure<SmtpConfig>(configuration.GetSection("Smtp"));
            services.Configure<WebApiConfig>(configuration.GetSection("WebApi"));
            services.Configure<WebAppConfig>(configuration.GetSection("WebApp"));

            services.Scan(scan =>
                scan.FromCallingAssembly()
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            // Localization.
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Routing.
            services.AddRouting(options => { options.LowercaseUrls = true; });

            // Customise default API behaviour.
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}
