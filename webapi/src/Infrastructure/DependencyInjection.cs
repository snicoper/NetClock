using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Common.Interfaces.Database;
using NetClock.Infrastructure.Persistence;

namespace NetClock.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var assembly = typeof(ApplicationDbContext).Assembly.FullName;

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(assembly)));

            // DI.
            services.Scan(scan =>
                scan.FromCallingAssembly()
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            // TODO: Experimental...
            // services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();

            return services;
        }
    }
}
