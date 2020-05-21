using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NetClock.Infrastructure.Persistence.Seeds
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();

            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.EnsureCreatedAsync();

            await SeedRoles.SeedAsync(serviceProvider, logger);
            await SeedPolicies.SeedAsync(serviceProvider, logger);
            await SeedUsers.SeedAsync(serviceProvider, logger);
        }
    }
}
