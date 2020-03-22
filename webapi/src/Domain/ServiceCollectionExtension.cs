using Microsoft.Extensions.DependencyInjection;

namespace NetClock.Domain
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            return services;
        }
    }
}
