using System.Reflection;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Common.Authorization;
using NetClock.Application.Common.Authorization.Handlers;
using NetClock.Application.Common.Behaviours;

namespace NetClock.Application
{
    public static class ApplicationRegistry
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.Scan(scan =>
                scan.FromCallingAssembly()
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            // Mapster.
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

            // FluentValidation.
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // MediatR.
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            // Authorization.
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            return services;
        }
    }
}
