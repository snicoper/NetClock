using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Common.Interfaces.Common;
using NetClock.Infrastructure.Persistence;
using NetClock.Infrastructure.Services.Common;

namespace NetClock.Infrastructure
{
    public static class InfrastructureRegistry
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Scan(scan =>
                scan.FromCallingAssembly()
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var assembly = typeof(ApplicationDbContext).Assembly.FullName;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(assembly)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IDomainEventService, DomainEventService>();

            return services;
        }
    }
}
