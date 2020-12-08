using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Common.Interfaces.Http;
using NetClock.WebApi.Services.Http;
using NetClock.WebApi.Validators;

namespace NetClock.WebApi.Extensions.ConfigureServices
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services)
        {
            services.AddTransient<IValidateParams, ValidateParams>();
            services.AddTransient(typeof(IResponseDataService<,>), typeof(ResponseDataService<,>));
            services.Scan(scan =>
                scan.FromCallingAssembly()
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return services;
        }
    }
}
