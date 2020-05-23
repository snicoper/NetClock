using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Authorization.Constants;

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

            if (await roleManager.Roles.AnyAsync())
            {
                return;
            }

            foreach (var role in roles)
            {
                var newRole = new IdentityRole { Name = role };
                await roleManager.CreateAsync(newRole);
                logger.LogInformation($"Role {role} creado con éxito");
            }
        }
    }
}
