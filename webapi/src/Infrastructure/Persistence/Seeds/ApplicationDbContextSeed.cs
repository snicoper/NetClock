using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NetClock.Infrastructure.Persistence.Seeds
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider, ApplicationDbContext applicationDbContext)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();
            await applicationDbContext.Database.EnsureCreatedAsync();

            await SeedRoles.SeedAsync(serviceProvider, logger);
            await SeedRoleClaims.SeedAsync(serviceProvider, logger);
            await SeedUsers.SeedAsync(serviceProvider, logger);
        }
    }
}
