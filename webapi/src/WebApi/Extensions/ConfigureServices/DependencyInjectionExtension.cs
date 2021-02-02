using Microsoft.Extensions.DependencyInjection;
using NetClock.WebApi.Validators;

namespace NetClock.WebApi.Extensions.ConfigureServices
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services)
        {
            services.AddTransient<IValidateParams, ValidateParams>();
            services.Scan(scan =>
                scan.FromCallingAssembly()
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return services;
        }
    }
}
