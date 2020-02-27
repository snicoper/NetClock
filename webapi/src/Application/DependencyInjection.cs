using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Common.Behaviours;
using NetClock.Application.Common.Configurations;
using NetClock.Application.Common.Interfaces.Http;
using NetClock.Application.Common.Services.Http;
using NetClock.Application.Common.Services.Identity;
using NetCore.AutoRegisterDi;

namespace NetClock.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            IConfiguration Configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IResponseDataService<,>), typeof(ResponseDataService<,>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<JwtConfig>(Configuration.GetSection("Jwt"));
            services.Configure<SmtpConfig>(Configuration.GetSection("Smtp"));
            services.Configure<WebApiConfig>(Configuration.GetSection("WebApi"));
            services.Configure<WebAppConfig>(Configuration.GetSection("WebApp"));

            services
                .RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(IdentityService)))
                .Where(c => c.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();

            return services;
        }
    }
}
