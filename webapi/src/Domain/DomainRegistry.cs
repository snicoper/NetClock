using Microsoft.Extensions.DependencyInjection;

namespace NetClock.Domain
{
    public static class DomainRegistry
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            return services;
        }
    }
}
