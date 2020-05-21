using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Authorization;

namespace NetClock.Infrastructure.Persistence.Seeds
{
    public static class SeedPolicies
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider, ILogger<ApplicationDbContext> logger)
        {
            // FIXME: Los tests no pasan
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Role Superuser, all permissions.
            var role = await roleManager.FindByNameAsync(AppRoles.Superuser);
            foreach (var permission in PermissionsHelper.GetAllPermissionValues())
            {
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
            }

            // Role Staff, all permissions.
            role = await roleManager.FindByNameAsync(AppRoles.Staff);
            foreach (var permission in PermissionsHelper.GetAllPermissionValues())
            {
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
            }

            // Role Employee.
            role = await roleManager.FindByNameAsync(AppRoles.Employee);
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Accounts.View));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Accounts.Update));
        }
    }
}
