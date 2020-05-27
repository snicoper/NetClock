using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Authorization.Constants;

namespace NetClock.Infrastructure.Persistence.Seeds
{
    public static class SeedRoleClaims
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider, ILogger<ApplicationDbContext> logger)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var permissions = PermissionsHelper.GetAllPermissionValues().ToArray();

            // Role Superuser, all permissions.
            var role = await roleManager.FindByNameAsync(AppRoles.Superuser);
            await AddClaimsAsync(roleManager, role, permissions);
            logger.LogInformation($"Se han creado los claims para el role {role.Name}");

            // Role Staff.
            role = await roleManager.FindByNameAsync(AppRoles.Staff);
            await AddClaimsAsync(roleManager, role, permissions);
            logger.LogInformation($"Se han creado los claims para el role {role.Name}");

            // Role Employee.
            role = await roleManager.FindByNameAsync(AppRoles.Employee);
            var permissionsRoleEmployee = new[]
            {
                Permissions.Accounts.View,
                Permissions.Accounts.Update
            };

            await AddClaimsAsync(roleManager, role, permissionsRoleEmployee);
            logger.LogInformation($"Se han creado los claims para el role {role.Name}");
        }

        private static async Task AddClaimsAsync(
            RoleManager<IdentityRole> roleManager,
            IdentityRole role,
            IEnumerable<string> claims)
        {
            foreach (var claim in claims)
            {
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, claim));
            }
        }
    }
}
