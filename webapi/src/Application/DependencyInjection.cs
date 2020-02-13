using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Common.Behaviours;
using NetClock.Application.Common.Interfaces.Http;
using NetClock.Application.Common.Services.Http;
using NetClock.Application.Common.Services.Identity;
using NetCore.AutoRegisterDi;

namespace NetClock.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IResponseDataService<,>), typeof(ResponseDataService<,>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services
                .RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(IdentityService)))
                .Where(c => c.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();

            return services;
        }
    }
}
