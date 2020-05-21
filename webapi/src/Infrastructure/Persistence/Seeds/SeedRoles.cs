using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Authorization;

namespace NetClock.Infrastructure.Persistence.Seeds
{
    public static class SeedRoles
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider, ILogger<ApplicationDbContext> logger)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[]
            {
                AppRoles.Superuser,
                AppRoles.Staff,
                AppRoles.Employee
            };

            foreach (var role in roles)
            {
                if (await roleManager.Roles.AnyAsync(r => r.Name == role))
                {
                    logger.LogInformation($"Role {role} ya existe en la base de datos");
                    continue;
                }

                await roleManager.CreateAsync(new IdentityRole { Name = role });
                logger.LogInformation($"Role {role} creado con éxito");
            }
        }
    }
}
